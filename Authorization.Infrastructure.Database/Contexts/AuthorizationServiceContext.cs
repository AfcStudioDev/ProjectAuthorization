using Authorization.Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Database.Contexts
{
    public class AuthorizationServiceContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public AuthorizationServiceContext( DbContextOptions<AuthorizationServiceContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<LicenseType>().HasData(
                    new LicenseType() { Id = 1, Duration = 30, Name = "30 дней", Price = 1499 },
                    new LicenseType() { Id = 2, Duration = 90, Name = "90 дней", Price = 3999 },
                    new LicenseType() { Id = 3, Duration = 180, Name = "180 дней", Price = 7199 },
                    new LicenseType() { Id = 4, Duration = 365, Name = "365 дней", Price = 12499 }
                );
        }
    }
}
