using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task CreateIndexIfNotExistsAsync(string indexName);
        Task<bool> CreateOrModify(Permission permission);
        Task<bool> CreateOrUpdateBulk(IEnumerable<Permission> permissions, string indexName);
        Task<Permission> GetByIdAsync(int id);
        Task<List<Permission>> GetAllAsync();
        Task<bool> Delete(int id);
    }
}
