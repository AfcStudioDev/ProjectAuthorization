using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Responses.Authorization;
using MediatR;
using System.Text.RegularExpressions;

namespace Authorization.Application.Domain.Handler.Authorization
{
    public class PostRegistrationHandler : IRequestHandler<PostRegistrationRequest, PostRegistrationResponse>
    {
        private readonly IRepository<Entities.User> _repository;
        private HashPassword _hasher;

        public PostRegistrationHandler(IRepository<Entities.User> repository, HashPassword hasher)
        {
            this._repository = repository;
            _hasher = hasher;
        }

        public async Task<PostRegistrationResponse> Handle(PostRegistrationRequest request, CancellationToken cancellationToken)
        {
            if (!ValidData(request))
            {
                return new PostRegistrationResponse() { Success = false, Message = "InValid Password or Login" };
            }
            if (CheckOnExist(request))
            {
                return new PostRegistrationResponse() { Success = false, Message = "User Exist" };
            }
            else
            {
                byte[] salt = _hasher.CreateDinamicSaltFromEmail(request.Email);
                Entities.User user = new Entities.User()
                {
                    Id = Guid.NewGuid(),//todo лучше id пусть база создает
                    Email = request.Email,
                    PasswordHash = _hasher.EncryptingPass(request.Password, salt)
                };

                try
                {
                    var countSaveEntities = _repository.Create(user);
                    if (countSaveEntities == 1)
                    {
                        return new PostRegistrationResponse() { Success = true };
                    }
                    else
                    {
                        return new PostRegistrationResponse() { Success = false, Message = "Error on add Entity" };
                    }
                }
                catch (Exception e)
                {
                    return new PostRegistrationResponse() { Success = false, Message = e.Message };
                }
            }
        }

        private bool CheckOnExist(PostRegistrationRequest request)
        {
            Entities.User user = _repository.Get().FirstOrDefault(u => u.Email == request.Email);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidData(PostRegistrationRequest request)
        {
            var emailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            var passwordRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");
            if (emailRegex.IsMatch(request.Email) && passwordRegex.IsMatch(request.Password))
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