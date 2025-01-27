using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Infrastructure.Permissions.Persistence
{
    public class PermissionRepository : IPermissionRepository
    {
        private static List<Permission> _permissions = new();
        public PermissionRepository()
        {
            _permissions.Add(new Permission() 
            { 
                Id = 123, 
                EmployerName = "Sergio", 
                EmployerLastName = "Rondon", 
                PermissionDate = new DateTime(2024, 12, 25), 
                PermissionTypeId = 1 
            });
        }
        public Task<Permission> GetByIdAsync(int Id)
        {
            var permission = _permissions.FirstOrDefault(p => p.Id == Id);
            return Task.FromResult(permission);
        }
    }
}
