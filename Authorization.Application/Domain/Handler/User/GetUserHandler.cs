using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Authorization.Application.Domain.Handler.User
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IRepository<Entities.User> _repository;

        public GetUserHandler( IRepository<Entities.User> repository )
        {
            _repository = repository;
        }
        public async Task<GetUserResponse> Handle( GetUserRequest request, CancellationToken cancellationToken )
        {
            GetUserResponse response = new GetUserResponse();

            response.User = await _repository.FindByIdAsync( (Guid)request.Id! );

            if (response.User != null)
            {
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
