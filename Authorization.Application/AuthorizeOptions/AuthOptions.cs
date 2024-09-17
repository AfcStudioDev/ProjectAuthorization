using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.AuthorizeOptions
{
	public class AuthOptions
	{
		public string Issuer { get; set; }

		public string Audience { get; set; }

		public string _key { get; set; }

		public int LifeTime { get; set; }

		public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey( Encoding.ASCII.GetBytes( _key ) );
	}
}
