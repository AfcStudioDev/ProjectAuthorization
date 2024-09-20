using Authorization.Application.AuthorizeOptions;

namespace HashPasswordTests
{
    public class HashPasswordTests
    {
        /// <summary>
        /// Проверяет логику генерации пароля
        /// </summary>
        /// Не забудь обновить соль, если обновил статик соль
        [Theory]
        [InlineData( "Test123!", "Server=1", new byte[] { 55, 48, 50, 49, 52, 100, 101, 101, 83, 101, 114, 118, 101, 114, 61, 49 }, "caWlZ4Od3W+Orhw2fgKNroL8LcqzyZXoKzVrFJ8sFg4=" )]
        public void EncryptingPass_GenerateDefaultPass_ReturnSame( string userPassword, string staticSalt, byte[] salt, string expected )
        {
            HashPassword hashPassword = new( staticSalt );

            string actual = hashPassword.EncryptingPass( userPassword, salt );

            Assert.Equal( expected, actual );
        }

        /// <summary>
        /// Проверяет логику генерации динамической соли
        /// </summary>
        /// Не забудь обновить соль, если обновил статик соль
        [Theory]
        [InlineData( "test1@test.test", "Server=1", new byte[] { 55, 48, 50, 49, 52, 100, 101, 101, 83, 101, 114, 118, 101, 114, 61, 49 } )]
        public void GenerateDynamicSalt_GenerateDefaultSalt_ReturnSame( string email, string staticSalt, byte[] expected )
        {
            HashPassword hashPassword = new( staticSalt );

            byte[] actual = hashPassword.CreateDinamicSaltFromEmail( email );

            Assert.Equal( expected, actual );
        }
    }
}
