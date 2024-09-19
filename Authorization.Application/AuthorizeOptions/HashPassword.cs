using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Authorization.Application.AuthorizeOptions
{
    public class HashPassword
    {
        private readonly byte[] _staticSalt = new byte[8];

        public HashPassword( string staticSaltString )
        {
            _staticSalt = Encoding.ASCII.GetBytes( staticSaltString.ToCharArray(), 0, 8 );
        }
        // TODO: очистить или найти применение
        //public string CreateSalt()
        //{
        //	var saltbyte = new byte[8];
        //	using ( var rng = RandomNumberGenerator.Create() )
        //	{
        //		rng.GetBytes( saltbyte );
        //	}
        //	string salt = Encoding.ASCII.GetString( saltbyte );

        //	return salt;
        //}

        public byte[] CreateDinamicSaltFromEmail( string email )
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash( Encoding.ASCII.GetBytes( email ) );
            string hashString = BitConverter.ToString( hashBytes ).Replace( "-", "" ).ToLower();
            string saltString = hashString.Substring( 0, 8 );
            byte[] dynamicSalt = Encoding.ASCII.GetBytes( saltString );
            byte[] fullSalt = dynamicSalt.Concat( _staticSalt ).ToArray();
            return fullSalt;
        }

        public string EncryptingPass( string password, byte[] salt )
        {
            string hash = Convert.ToBase64String( KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
                ) );
            return hash;
        }
    }
}