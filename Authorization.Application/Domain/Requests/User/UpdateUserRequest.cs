using Authorization.Application.Domain.Responses.User;

using MediatR;
using System;

namespace Authorization.Application.Domain.Requests.User
{
    public class UpdateUserRequest : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
    }
}
