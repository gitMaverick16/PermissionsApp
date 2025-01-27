using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Infrastructure.Permissions.Persistence
{
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly static List<Permission> _permissions = new();
        public Task AddPermissionAsync(Permission permission)
        {
            _permissions.Add(permission);
            return Task.CompletedTask;
        }
    }
}
