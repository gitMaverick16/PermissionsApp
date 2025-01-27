using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Infrastructure.Common.Persistence.Seeds
{
    public class PermissionSeed : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission()
                {
                    Id = 1,
                    EmployerName = "Jhon",
                    EmployerLastName = "Doe",
                    PermissionDate = new DateTime(2025,3,15),
                    PermissionTypeId = 1
                },
                new Permission()
                {
                    Id = 2,
                    EmployerName = "Jane",
                    EmployerLastName = "Smith",
                    PermissionDate = new DateTime(2025, 7, 10),
                    PermissionTypeId = 2 
                },
                new Permission()
                {
                    Id = 3,
                    EmployerName = "Emily",
                    EmployerLastName = "Johnson",
                    PermissionDate = new DateTime(2025, 5, 20),
                    PermissionTypeId = 3
                },
                new Permission()
                {
                    Id = 4,
                    EmployerName = "Olivia",
                    EmployerLastName = "Brown",
                    PermissionDate = new DateTime(2025, 4, 5),
                    PermissionTypeId = 5
                },
                new Permission()
                {
                    Id = 5,
                    EmployerName = "Lucas",
                    EmployerLastName = "Martinez",
                    PermissionDate = new DateTime(2025, 8, 15),
                    PermissionTypeId = 4 
                }
            );
        }
    }
}
