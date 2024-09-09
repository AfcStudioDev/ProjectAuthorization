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

        public CheckLicenseHandler(IRepository<Device> repository)
        {
            _repository = repository;
        }

        public async Task<CheckLicenseResponse> Handle(CheckLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = new CheckLicenseResponse();

            var device = _repository.Get(a => a.DeviceNumber == request.DeviceNumber && a.LicenseKey == request.LicenseKey).FirstOrDefault();

            if(device is not null)
            {
                var license = device.Licenses.Where(a => a.StartLicense + a.LicenseType.Length >= DateTime.Now).OrderByDescending(a => a.StartLicense).FirstOrDefault();

                if(license is not null)
                {
                    response.Success = true;
                    response.Message = $"Лицензия до {(license.StartLicense + license.LicenseType.Length).Date}";
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
