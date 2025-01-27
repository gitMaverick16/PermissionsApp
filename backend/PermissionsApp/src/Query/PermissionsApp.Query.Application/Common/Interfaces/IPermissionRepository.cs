using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByIdAsync(int Id);
    }
}
