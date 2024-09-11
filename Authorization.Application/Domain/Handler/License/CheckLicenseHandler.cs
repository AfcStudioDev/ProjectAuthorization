using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CheckLicenseHandler : IRequestHandler<CheckLicenseRequest, CheckLicenseResponse>
    {
        private readonly IRepository<Device> _repository;
        private readonly IRepository<Entities.LicenseType> _licenseTypeRepository;

        public CheckLicenseHandler(IRepository<Device> repository, IRepository<Entities.LicenseType> licenseTypeRepository)
        {
            _repository = repository;
            _licenseTypeRepository = licenseTypeRepository;
        }

        public async Task<CheckLicenseResponse> Handle(CheckLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = new CheckLicenseResponse();

            var device = _repository.GetWithInclude(a => a.DeviceNumber == request.DeviceNumber && a.LicenseKey == request.LicenseKey, a => a.Licenses).FirstOrDefault();

            if (device is not null)
            {
                foreach (var licensed in device.Licenses)
                {
                    licensed.LicenseType = await _licenseTypeRepository.FindByIdAsync(licensed.LicenseTypeId);
                }

                var license = device.Licenses.Where(a => a.StartLicense.AddDays(a.LicenseType.Duration) >= DateTime.Now).OrderByDescending(a => a.StartLicense).FirstOrDefault();

                if (license is not null)
                {
                    response.Success = true;
                    response.Message = $"Лицензия до {(license.StartLicense.AddDays(license.LicenseType.Duration)).Date}";
                }
            }
            else
            {
                response.Success = false;
            }

            return response;
        }
    }
}
