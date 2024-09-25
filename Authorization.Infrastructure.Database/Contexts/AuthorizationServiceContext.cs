using Authorization.Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;

namespace Authorization.Infrastructure.Database.Contexts
{
    public class AuthorizationServiceContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public AuthorizationServiceContext( DbContextOptions<AuthorizationServiceContext> options ) : base( options )
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<LicenseType>().HasData(
                    new LicenseType() { Id = Guid.NewGuid(), Duration = 30, Name = "месячная подписка", Price = 1499 },
                    new LicenseType() { Id = Guid.NewGuid(), Duration = 90, Name = "квартальная подписка", Price = 3999 },
                    new LicenseType() { Id = Guid.NewGuid(), Duration = 180, Name = "полугодовая подписка", Price = 7199 },
                    new LicenseType() { Id = Guid.NewGuid(), Duration = 365, Name = "годовая подписка", Price = 12499 }
                );
        }
    }
}
