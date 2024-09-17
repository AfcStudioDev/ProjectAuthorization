using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;
using MediatR;

namespace Authorization.Application.Domain.Handler.User
{
    public class GetListUserHandler : IRequestHandler<GetListUserRequest, GetListUserResponse>
    {
        private readonly IRepository<Entities.User> _repository;

        public GetListUserHandler(IRepository<Entities.User> repository)
        {
            _repository = repository;
        }

        public async Task<GetListUserResponse> Handle(GetListUserRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Entities.User> users;

            users = _repository.Get();

            if (request.Email != null)
            {
                users = users.Where(a => a.Email == request.Email);
            }
            return new GetListUserResponse() { Success = true, Users = users.ToList() };
        }
    }
}
