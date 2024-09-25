using System.Reflection;

using Authorization.Application.AuthorizeOptions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application
{
    public static class ServiceCollection
    {
        public static void AddApplication( this IServiceCollection services, IConfiguration configuration )
        {
            Assembly assembly = typeof( ServiceCollection ).GetTypeInfo().Assembly;
            _ = services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies( Assembly.GetExecutingAssembly() ) );
            //services.AddMediatR(Assembly.GetExecutingAssembly()); почемуто это не саботало. сработала строка выше 

            _ = services.AddSingleton( configuration.GetSection( "AuthOptions" ).Get<AuthOptions>() );
            _ = services.AddSingleton( x => new HashPassword( configuration.GetConnectionString( "DefaultConnection" )! ) );
            _ = services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                    .AddJwtBearer( options =>
                    {
                        AuthOptions authOptions = configuration.GetSection( "AuthOptions" ).Get<AuthOptions>();
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,

                            ValidateLifetime = true,

                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    } );
        }
    }
}
