using Microsoft.Extensions.DependencyInjection;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Infrastructure.Permissions.Persistence;

namespace PermissionsApp.Command.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            return services;
        }
    }
}
