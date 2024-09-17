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

        public PostLoginHandler(IRepository<Entities.User> repository, HashPassword hasher, AuthOptions authOptions)
        {
            _repository = repository;
            _hasher = hasher;
            _authOptions = authOptions;
        }

        public async Task<PostLoginResponse> Handle(PostLoginRequest request, CancellationToken cancellationToken)
        {
            if (CheckOnIdentity(request.Identificator, request.Password, out Entities.User user))
            {
                TokenCreater tokenCreater = new TokenCreater(_authOptions);
                string token = tokenCreater.CreateToken(user);

                return new PostLoginResponse() { Success = true, Token = token };
            }
            else
            {
                return new PostLoginResponse() { Success = false, Message = "Bad Login or Pass" };
            }
        }

        private bool CheckOnIdentity(string identity, string password, out Entities.User user)
        {
            user = _repository.Get().SingleOrDefault(u => u.Email == identity);
            if (user == null)
            {
                return false;
            }
            var salt = _hasher.CreateDinamicSaltFromEmail(user.Email);
            string passwordHash = _hasher.EncryptingPass(password, salt);

            if (user.PasswordHash == passwordHash)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
