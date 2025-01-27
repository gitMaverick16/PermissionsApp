using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionsApp.Command.Domain.Permissions;
using System.Reflection.Emit;

namespace PermissionsApp.Command.Infrastructure.Common.Persistence.Seeds
{
    public class PermissionTypeSeed : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.HasData(
                new PermissionType()
                {
                    Id = 1,
                    Description = "Sick Leave"
                },
                new PermissionType()
                {
                    Id = 2,
                    Description = "Vacation"
                },
                new PermissionType()
                {
                    Id = 3,
                    Description = "Maternity Leave"
                },
                new PermissionType()
                {
                    Id = 4,
                    Description = "Paternity Leave"
                },
                new PermissionType()
                {
                    Id = 5,
                    Description = "Personal Leave"
                },
                new PermissionType()
                {
                    Id = 6,
                    Description = "Bereavement Leave"
                },
                new PermissionType()
                {
                    Id = 7,
                    Description = "Medical Leave"
                }
            );
        }
    }
}
