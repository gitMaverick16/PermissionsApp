using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;
using PermissionsApp.Command.Infrastructure.Common.Persistence;

namespace PermissionsApp.Command.Infrastructure.Permissions.Persistence
{
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly PermissionsAppDbContext _dbContext;
        public PermissionRepository(PermissionsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddPermissionAsync(Permission permission)
        {
            await _dbContext.Permissions.AddAsync(permission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
