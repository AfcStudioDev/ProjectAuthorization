using Authorization.Application.Domain.Responses.User;

using MediatR;
using System;

namespace Authorization.Application.Domain.Requests.User
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }
    }
}
