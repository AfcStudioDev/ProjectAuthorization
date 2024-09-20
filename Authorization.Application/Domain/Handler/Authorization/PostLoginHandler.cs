using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Responses.Authorization;

using MediatR;

namespace Authorization.Application.Domain.Handler.Authorization
{
    public class PostLoginHandler : IRequestHandler<PostLoginRequest, PostLoginResponse>
    {
        private readonly IRepository<Entities.User> _repository;
        private readonly HashPassword _hasher;
        private readonly AuthOptions _authOptions;

        public PostLoginHandler( IRepository<Entities.User> repository, HashPassword hasher, AuthOptions authOptions )
        {
            _repository = repository;
            _hasher = hasher;
            _authOptions = authOptions;
        }

        public async Task<PostLoginResponse> Handle( PostLoginRequest request, CancellationToken cancellationToken )
        {
            PostLoginResponse loginResponse =  new() { Success = false, Message = "Bad Login or Pass" };

            bool userIsFine = CheckOnIdentity(request.Identificator, request.Password, out Entities.User user);
            if ( user == null)
            {
                loginResponse.Message = "User not found";
            }

            if (userIsFine)
            {
                TokenCreater tokenCreater = new TokenCreater( _authOptions );
                string token = tokenCreater.CreateToken( user );
                loginResponse.Success = true;
                loginResponse.Token = token;
                loginResponse.Message = null;
            }
            //else if(user == null)
            //{
            //    return new PostLoginResponse() { Success = false, Message = "User not found" };
            //}

            return loginResponse;
            //else
            //{
            //    return new PostLoginResponse() { Success = false, Message = "Bad Login or Pass" };
            //}
        }

        private bool CheckOnIdentity( string identity, string password, out Entities.User user )
        {
            var isExist = false;
            user = _repository.Get().SingleOrDefault( repoUser => repoUser.Email == identity );
            if (user != null)
            {
                byte[] salt = _hasher.CreateDinamicSaltFromEmail( user.Email );
                string passwordHash = _hasher.EncryptingPass( password, salt );
                isExist = user.PasswordHash == passwordHash;
            }

            return isExist;
            //if (user.PasswordHash == passwordHash)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
