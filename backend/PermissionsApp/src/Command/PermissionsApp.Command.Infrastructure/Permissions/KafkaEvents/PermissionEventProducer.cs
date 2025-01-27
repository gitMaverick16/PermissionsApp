using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Infrastructure.Configurations;

namespace PermissionsApp.Command.Infrastructure.Permissions.KafkaEvents
{
    public class PermissionEventProducer : IPermissionEventProducer
    {
        public KafkaSettings _kafkaSettings;
        public PermissionEventProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }

        public void Produce(string topic, PermissionEvent permission)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{_kafkaSettings.HostName}:{_kafkaSettings.Port}"
            };

            using( var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string value = JsonConvert.SerializeObject(permission);
                var message = new Message<Null, string>
                {
                    Value = value
                };
                producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
            }
        }
    }
}
