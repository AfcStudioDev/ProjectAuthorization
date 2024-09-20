using System.Text.RegularExpressions;

using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Responses.Authorization;

using MediatR;

namespace Authorization.Application.Domain.Handler.Authorization
{
    public class PostRegistrationHandler : IRequestHandler<PostRegistrationRequest, PostRegistrationResponse>
    {
        private readonly IRepository<Entities.User> _repository;
        private readonly HashPassword _hasher;

        public PostRegistrationHandler( IRepository<Entities.User> repository, HashPassword hasher )
        {
            this._repository = repository;
            _hasher = hasher;
        }

        public async Task<PostRegistrationResponse> Handle( PostRegistrationRequest request, CancellationToken cancellationToken )
        {
            PostRegistrationResponse response = new() { Success = false};

            if (!ValidData( request ))
            {
                response.Message = "InValid Password or Login";
            }
            else
            {
                if (IsExist( request ))
                {
                    response.Message = "User Exist";
                }
                else
                {
                    Entities.User user = CreateUserForDB( request );

                    try
                    {
                        int countSaveEntities = _repository.Create( user );
                        if (countSaveEntities == 1)
                        {
                            response.Success = true;
                        }
                        else
                        {
                            response.Message = "Error on add Entity";
                        }
                    }
                    catch (Exception e)
                    {
                        response.Message = e.Message;
                    }
                }
            }

            return response;
        }

        private Entities.User CreateUserForDB( PostRegistrationRequest request )
        {
            byte[] salt = _hasher.CreateDinamicSaltFromEmail( request.Email );
            Entities.User user = new Entities.User()
            {
                Id = Guid.NewGuid(),//todo лучше id пусть база создает
                Email = request.Email,
                PasswordHash = _hasher.EncryptingPass( request.Password, salt )
            };
            return user;
        }

        private bool IsExist( PostRegistrationRequest request )
        {
            Entities.User user = _repository.Get().FirstOrDefault( u => u.Email == request.Email );
            //if (user != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return user != null ? true : false;
        }

        private bool ValidData( PostRegistrationRequest request )
        {
            var isDataValid = false;
            if (request.Email.Length < 31)
            {
                var emailRegex = new Regex( "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$" );
                var passwordRegex = new Regex( "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$" );

                if (emailRegex.IsMatch( request.Email ) && passwordRegex.IsMatch( request.Password ))
                {
                    isDataValid = true;
                }
            }

            return isDataValid;
        }
    }
}