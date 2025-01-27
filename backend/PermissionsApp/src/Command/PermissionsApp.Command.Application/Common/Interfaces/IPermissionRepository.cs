using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task AddPermissionAsync(Permission permission);
        Task<Permission?> GetByIdAsync(int id);
        Task RemovePermissionAsync(Permission permission);
        Task UpdateAsync(Permission permission);
    }
}
