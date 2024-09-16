using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;
using MediatR;

namespace Authorization.Application.Domain.Handler.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IMediator _mediator;
        public CreateUserHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var registration = new PostRegistrationRequest() { Email = request.Email, Password = request.Password };

            var registrationResponse = await _mediator.Send(registration);

            var response = new CreateUserResponse();

            if (registrationResponse != null)
            {
                if (registrationResponse.Success)
                    response.Success = true;
                else
                {
                    response.Success = false;
                    response.Message = registrationResponse.Message;
                }
            }

            return response;
        }
    }
}
