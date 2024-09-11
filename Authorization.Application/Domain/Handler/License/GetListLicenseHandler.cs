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
        private readonly IRepository<Entities.LicenseType> _licenseTypeRepository;

        public GetListLicenseHandler(IRepository<Device> repository, IRepository<Entities.LicenseType> repositoryLicenseType)
        {
            _repository = repository;
            _licenseTypeRepository = repositoryLicenseType;
        }

        public async Task<GetListLicenseResponse> Handle(GetListLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = _repository.GetWithInclude(a => a.UserId == request.UserId, a => a.Licenses).ToList();

            foreach (var device in response)
            {
                foreach (var license in device.Licenses)
                {
                    license.LicenseType = await _licenseTypeRepository.FindByIdAsync(license.LicenseTypeId);
                }
            }

            return new GetListLicenseResponse() { Licenses = response, Success = true };
        }
    }
}
