using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;

using MediatR;

namespace Authorization.Application.Domain.Handler.User
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IRepository<Entities.User> _reposiotory;
        private readonly HashPassword _hasher;

        public UpdateUserHandler( IRepository<Entities.User> repository, HashPassword hash )
        {
            _reposiotory = repository;
            _hasher = hash;
        }

        public async Task<UpdateUserResponse> Handle( UpdateUserRequest request, CancellationToken cancellationToken )
        {
            UpdateUserResponse response = new UpdateUserResponse();

            Entities.User user = await _reposiotory.FindByIdAsync( request.Id );

            if (user != null)
            {
                user.Email = request.Email;

                if (request.Password is not null)
                {
                    byte[] salt = _hasher.CreateDinamicSaltFromEmail( request.Email );
                    user.PasswordHash = _hasher.EncryptingPass( request.Password, salt );
                }

                _ = _reposiotory.Update( user );

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Пользователь не найден";
            }

            return response;
        }
    }
}
