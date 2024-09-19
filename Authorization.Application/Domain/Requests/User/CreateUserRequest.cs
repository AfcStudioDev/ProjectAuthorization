using Authorization.Application.Domain.Responses.User;

using MediatR;

namespace Authorization.Application.Domain.Requests.User
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}