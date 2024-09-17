using Authorization.Application.Domain.Responses.User;
using MediatR;

namespace Authorization.Application.Domain.Requests.User
{
    public class GetListUserRequest : IRequest<GetListUserResponse>
    {
        public string? Email { get; set; }
    }
}
