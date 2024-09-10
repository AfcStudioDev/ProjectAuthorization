using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Authorization.Application.Domain.Entities;

namespace Authorization.Application.AuthorizeOptions
{
	public class TokenCreater
	{
		private AuthOptions _authOptions;
		public TokenCreater( AuthOptions authOptions )
		{
			_authOptions = authOptions;
		}

		public string CreateToken( User user )
		{
			List<Claim> claims = GetClaims( user );
			var jwt = new JwtSecurityToken(
					issuer: _authOptions.Issuer,
					audience: _authOptions.Audience,
					claims: claims,
					expires: DateTime.UtcNow.Add( TimeSpan.FromMinutes( _authOptions.LifeTime ) ),
					signingCredentials: new SigningCredentials( _authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 ) );

			return new JwtSecurityTokenHandler().WriteToken( jwt );
		}

		public List<Claim> GetClaims( User user )
		{
			var claims = new List<Claim>
			{
				new Claim("UserId",user.Id.ToString()),
				new Claim("Email",user.Email)
			};
			return claims;
		}
	}
}
