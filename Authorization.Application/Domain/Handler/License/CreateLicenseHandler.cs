using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CreateLicenseHandler : IRequestHandler<CreateLicenseRequest, CreateLicenseResponse>
    {
        private readonly IRepository<Device> _deviceRepository;
        private readonly IRepository<Entities.License> _licenseRepository;

        public CreateLicenseHandler(IRepository<Device> deviceRepository, IRepository<Entities.License> entitiesRepository)
        {
            _deviceRepository = deviceRepository;
            _licenseRepository = entitiesRepository;
        }

        public async Task<CreateLicenseResponse> Handle(CreateLicenseRequest request, CancellationToken cancellationToken)
        {
            var newLicense = new Entities.License() { LicenseTypeId = request.LicenseType, StartLicense = DateTime.Now };
            var device = _deviceRepository.Get(a => a.UserId == request.UserId).FirstOrDefault();

            if (device is not null)
            {
                var lastLicense = device.Licenses.OrderByDescending(a => a.StartLicense).FirstOrDefault();
                if (lastLicense != null)
                {
                    newLicense.StartLicense = lastLicense.StartLicense + lastLicense.LicenseType.Length;
                }
            }
            else
            {
                device = new Device() { DeviceNumber = request.DeviceNumber, UserId = request.UserId };
                device.Licenses.Add(newLicense);

                await _deviceRepository.CreateAsync(device);
            }

            await _deviceRepository.UpdateAsync(device);

            return new CreateLicenseResponse() { Success = true };
        }
    }
}
