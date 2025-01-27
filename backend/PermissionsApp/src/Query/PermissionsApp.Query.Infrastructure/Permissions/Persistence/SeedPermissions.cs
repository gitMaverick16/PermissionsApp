using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Infrastructure.Permissions.Persistence
{
    public class SeedPermissions : ISeedPermissions
    {
        private readonly IPermissionRepository _permissionRepository;

        public SeedPermissions(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task SeedPermission()
        {
            await _permissionRepository.CreateIndexIfNotExistsAsync("permissions");
            var permission = await _permissionRepository.GetByIdAsync(1);
            if (permission is not null)
            {
                return;
            }
            var permissions = new List<Permission>
            {
                new Permission { Id = 1, EmployerName = "John", EmployerLastName = "Doe", PermissionDate = new DateTime(2025, 3, 15), PermissionType = new PermissionType{ Id = 1, Description= "Sick Leave"} },
                new Permission { Id = 2, EmployerName = "Jane", EmployerLastName = "Smith", PermissionDate = new DateTime(2025, 7, 10), PermissionType = new PermissionType{ Id = 2, Description= "Vacation"} },
                new Permission { Id = 3, EmployerName = "Emily", EmployerLastName = "Johnson", PermissionDate = new DateTime(2025, 5, 20), PermissionType = new PermissionType{ Id = 3, Description= "Maternity Leave"} },
                new Permission { Id = 4, EmployerName = "Olivia", EmployerLastName = "Brown", PermissionDate = new DateTime(2025, 4, 5), PermissionType = new PermissionType{ Id = 4, Description= "Paternity Leave"} },
                new Permission { Id = 5, EmployerName = "Lucas", EmployerLastName = "Martinez", PermissionDate = new DateTime(2025, 8, 15), PermissionType = new PermissionType{ Id = 5, Description= "Personal Leave"} }
            };
            foreach (var p in permissions)
            {
                await _permissionRepository.CreateOrModify(p);
            }
        }
    }
}
