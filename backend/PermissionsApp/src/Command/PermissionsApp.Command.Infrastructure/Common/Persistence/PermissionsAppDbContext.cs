using Microsoft.EntityFrameworkCore;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;
using PermissionsApp.Command.Infrastructure.Common.Persistence.Seeds;

namespace PermissionsApp.Command.Infrastructure.Common.Persistence
{
    public class PermissionsAppDbContext : DbContext, IUnitOfWork
    {
        public PermissionsAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PermissionType>()
                .HasKey(pt => pt.Id);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new PermissionTypeSeed());
            modelBuilder.ApplyConfiguration(new PermissionSeed());
        }
    }
}
