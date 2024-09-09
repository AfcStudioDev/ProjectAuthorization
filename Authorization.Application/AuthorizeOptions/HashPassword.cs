using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Authorization.Application.AuthorizeOptions
{
	public class HashPassword
	{
		private readonly byte[] _staticSalt = new byte[8]; //{ 130, 105, 75, 103, 70, 158, 128, 199 };	 

		public HashPassword( string staticSaltString )
		{
			_staticSalt = Encoding.ASCII.GetBytes( staticSaltString.ToCharArray(), 0, 8 );
		}

		public string CreateSalt()
		{
			var saltbyte = new byte[8];
			using ( var rng = RandomNumberGenerator.Create() )
			{
				rng.GetBytes( saltbyte );
			}
			string salt = Encoding.ASCII.GetString( saltbyte );

			return salt;
		}

		private byte[] GetSalt( string salt )
		{
			byte[] dynamicSalt = Encoding.ASCII.GetBytes( salt.ToCharArray(), 0, 8 );
			var fullSalt = dynamicSalt.Concat( _staticSalt ).ToArray();
			return fullSalt;
		}

		public string EncryptingPass( string password, string salt )
		{
			byte[] fullSalt = GetSalt( salt );
			var hash = Convert.ToBase64String( KeyDerivation.Pbkdf2(
				password: password,
				salt: fullSalt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 32
				) );
			return hash;
		}
	}
}
