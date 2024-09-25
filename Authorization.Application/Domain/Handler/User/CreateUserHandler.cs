using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;

using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Authorization.Application.Domain.Handler.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IMediator _mediator;
        public CreateUserHandler( IMediator mediator )
        {
            _mediator = mediator;
        }

        public async Task<CreateUserResponse> Handle( CreateUserRequest request, CancellationToken cancellationToken )
        {
            PostRegistrationRequest registration = new PostRegistrationRequest() { Email = request.Email, Password = request.Password };

            Responses.Authorization.PostRegistrationResponse registrationResponse = await _mediator.Send( registration );

            CreateUserResponse response = new CreateUserResponse();

            if (registrationResponse != null)
            {
                if (registrationResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    response.Message = registrationResponse.Message;
                }
            }

            return response;
        }
    }
}
