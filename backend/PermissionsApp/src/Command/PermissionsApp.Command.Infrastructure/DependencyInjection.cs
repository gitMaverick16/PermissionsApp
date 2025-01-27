using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Infrastructure.Common.Persistence;
using PermissionsApp.Command.Infrastructure.Permissions.Persistence;

namespace PermissionsApp.Command.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PermissionsAppDatabase");
            services.AddDbContext<PermissionsAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            return services;
        }
    }
}
