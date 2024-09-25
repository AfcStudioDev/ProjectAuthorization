using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Authorization.Application.Domain.Entities;

using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.AuthorizeOptions
{
    public class TokenCreater
    {
        private readonly AuthOptions _authOptions;
        public TokenCreater( AuthOptions authOptions )
        {
            _authOptions = authOptions;
        }

        public string CreateToken( User user )
        {
            List<Claim> claims = GetClaims( user );
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add( TimeSpan.FromMinutes( _authOptions.LifeTime ) ),
                    signingCredentials: new SigningCredentials( _authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 ) );

            return new JwtSecurityTokenHandler().WriteToken( jwt );
        }

        public List<Claim> GetClaims( User user )
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("Email",user.Email)
            };
            return claims;
        }
    }
}
