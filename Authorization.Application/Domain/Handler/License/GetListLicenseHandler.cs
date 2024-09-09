using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class GetListLicenseHandler : IRequestHandler<GetListLicenseRequest, GetListLicenseResponse>
    {
        private readonly IRepository<Device> _repository;

        public GetListLicenseHandler(IRepository<Device> repository)
        {
            _repository = repository;
        }

        public async Task<GetListLicenseResponse> Handle(GetListLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = _repository.GetWithInclude(a => a.UserId == request.UserId, a => a.Licenses).ToList();

            return new GetListLicenseResponse() { Licenses = response, Success = true };
        }
    }
}
