using Authorization.Application.Domain.Responses.Authorization;

using MediatR;

namespace Authorization.Application.Domain.Requests.Authorization
{
    public class PostRegistrationRequest : IRequest<PostRegistrationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
