using Microsoft.EntityFrameworkCore;
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
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _dbContext.Permissions.FirstOrDefaultAsync(permission => permission.Id == id);
        }

        public Task RemovePermissionAsync(Permission permission)
        {
            _dbContext.Remove(permission);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Permission permission)
        {
            _dbContext.Update(permission);

            return Task.CompletedTask;
        }
    }
}
