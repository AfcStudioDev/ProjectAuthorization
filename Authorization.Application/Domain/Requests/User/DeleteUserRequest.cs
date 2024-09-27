using Authorization.Application.Domain.Responses.User;

using MediatR;

namespace Authorization.Application.Domain.Requests.User
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        public uint Id { get; set; }
    }
}
