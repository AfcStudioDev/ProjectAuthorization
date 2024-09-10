using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Authorization.Application.AuthorizeOptions;

using MediatR;
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
			var assembly = typeof( ServiceCollection ).GetTypeInfo().Assembly;
			services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies( Assembly.GetExecutingAssembly() ) );
			//services.AddMediatR(Assembly.GetExecutingAssembly()); почемуто это не саботало. сработала строка выше 

			services.AddSingleton( configuration.GetSection( "AuthOptions" ).Get<AuthOptions>() );
			services.AddSingleton( x => new HashPassword( configuration["DefaultConnection"] ) );
			services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
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
