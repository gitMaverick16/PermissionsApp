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
                    EmployeeName = "Jhon",
                    EmployeeLastName = "Doe",
                    PermissionDate = new DateTime(2025,3,15),
                    PermissionTypeId = 1
                },
                new Permission()
                {
                    Id = 2,
                    EmployeeName = "Jane",
                    EmployeeLastName = "Smith",
                    PermissionDate = new DateTime(2025, 7, 10),
                    PermissionTypeId = 2 
                },
                new Permission()
                {
                    Id = 3,
                    EmployeeName = "Emily",
                    EmployeeLastName = "Johnson",
                    PermissionDate = new DateTime(2025, 5, 20),
                    PermissionTypeId = 3
                },
                new Permission()
                {
                    Id = 4,
                    EmployeeName = "Olivia",
                    EmployeeLastName = "Brown",
                    PermissionDate = new DateTime(2025, 4, 5),
                    PermissionTypeId = 5
                },
                new Permission()
                {
                    Id = 5,
                    EmployeeName = "Lucas",
                    EmployeeLastName = "Martinez",
                    PermissionDate = new DateTime(2025, 8, 15),
                    PermissionTypeId = 4 
                }
            );
        }
    }
}
