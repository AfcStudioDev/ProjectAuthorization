using Authorization.Application.Domain.Responses.User;

using MediatR;

namespace Authorization.Application.Domain.Requests.User
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public Guid? Id { get; set; }
    }
}
