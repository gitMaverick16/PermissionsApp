using Microsoft.Extensions.DependencyInjection;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Infrastructure.Permissions.Persistence;

namespace PermissionsApp.Query.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            //services.AddScoped<ISeedPermissions, SeedPermissions>();

            return services;
        }
    }
}
