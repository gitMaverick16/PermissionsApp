using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task AddPermissionAsync(Permission permission);
    }
}
