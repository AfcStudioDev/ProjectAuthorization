using Authorization.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Database.Contexts
{
    public class AuthorizationServiceContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public AuthorizationServiceContext(DbContextOptions<AuthorizationServiceContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
