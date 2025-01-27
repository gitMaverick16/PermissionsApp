using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermissionsApp.Command.Application.Permissions.Commands.CreatePermission;
using PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                options.AddBehavior<
                    IPipelineBehavior<CreatePermissionCommand, ErrorOr<Permission>>, 
                    CreatePermissionCommandBehavior>();
                options.AddBehavior<
                    IPipelineBehavior<ModifyPermissionCommand, ErrorOr<Permission>>,
                   ModifyPermissionCommandBehavior>();
            });
            return services;
        }
    }
}
