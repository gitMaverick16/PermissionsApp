using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;
using PermissionsApp.Query.Infrastructure.Events;
using System.Text.Json;

namespace PermissionsApp.Query.Infrastructure.Consumer
{
    public class KafkaConsumer : IHostedService
    {
        private const string BootstrapServers = "localhost:9092"; 
        private const string Topic = "permissions_topic";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public KafkaConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => StartListening(), cancellationToken);
            return Task.CompletedTask;
        }

        public void StartListening(CancellationToken cancellationToken = default)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = BootstrapServers,
                GroupId = "permission_group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(Topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            var permission = JsonSerializer.Deserialize<PermissionEvent>(consumeResult.Message.Value);
                            Console.WriteLine($"Message received: {permission.EmployeeName} {permission.EmployeeLastName}, {permission.PermissionDate}");
                            SynchronizeQueryDatabase(permission);
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error consuming message: {e.Message}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Consuming stopped.");
                }
                finally
                {
                    consumer.Close();
                }
            }
        }

        public void SynchronizeQueryDatabase(PermissionEvent @event)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var permissionsRepositoryService = scope.ServiceProvider.GetService<IPermissionRepository>();
                if(@event.Action == ActionType.Add || @event.Action == ActionType.Modify)
                {
                    var permission = new Permission
                    {
                        Id = @event.Id,
                        EmployeeName = @event.EmployeeName,
                        EmployeeLastName = @event.EmployeeLastName,
                        PermissionDate = @event.PermissionDate,
                        PermissionType = new PermissionType
                        {
                            Id = @event.PermissionTypeId,
                            Description = @event.PermissionTypeDescription
                        }
                    };
                    permissionsRepositoryService!.CreateOrModify(permission);
                }else if(@event.Action == ActionType.Delete)
                {
                    permissionsRepositoryService!.Delete(@event.Id);
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
