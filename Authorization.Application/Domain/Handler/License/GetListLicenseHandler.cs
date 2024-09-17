using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class GetListLicenseHandler : IRequestHandler<GetListLicenseRequest, GetListLicenseResponse>
    {
        private readonly IRepository<Entities.User> _repository;

        public GetListLicenseHandler(IRepository<Entities.User> repository)
        {
            _repository = repository;
        }

        public async Task<GetListLicenseResponse> Handle(GetListLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = _repository.GetWithInclude(a => a.Id == request.UserId, a => a.Licenses).First().Licenses;

            return new GetListLicenseResponse() { Licenses = response, Success = true };
        }
    }
}
