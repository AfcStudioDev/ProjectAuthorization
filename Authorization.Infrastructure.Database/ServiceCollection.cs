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
        public static void AddInfrastructureDataBase( this IServiceCollection services, IConfiguration configuration )
        {
            string connectionString = configuration.GetConnectionString( "DefaultConnection" )!;

            _ = services.AddDbContext<AuthorizationServiceContext>( options =>
                options.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) )
            );

            _ = services.AddTransient<IRepository<User>, UserRepository>();
            _ = services.AddTransient<IRepository<Device>, DeviceRepository>();
            _ = services.AddTransient<IRepository<LicenseType>, LicenseTypeRepository>();
        }
    }
}
