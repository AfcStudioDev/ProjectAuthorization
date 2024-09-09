using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Infrastructure.Database.Contexts;
using Authorization.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Infrastructure.Database
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthorizationServiceContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21)))
            );

            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<License>, LicenseRepository>();
            services.AddTransient<IRepository<LicenseType>, LicenseTypeRepository>();
            services.AddTransient<IRepository<Device>, DeviceRepository>();
        }
    }
}
