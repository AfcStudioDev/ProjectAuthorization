using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;

using MediatR;

namespace Authorization.Application.Domain.Handler.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IRepository<Entities.User> _userRepository;

        public DeleteUserHandler( IRepository<Entities.User> repository )
        {
            _userRepository = repository;
        }

        public async Task<DeleteUserResponse> Handle( DeleteUserRequest request, CancellationToken cancellationToken )
        {
            DeleteUserResponse response = new DeleteUserResponse();

            Entities.User user = await _userRepository.FindByIdAsync( request.Id );
            if (user != null)
            {
                _ = await _userRepository.RemoveAsync( user );
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
