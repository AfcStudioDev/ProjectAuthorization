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
            var device = _deviceRepository.GetWithInclude(a => a.UserId == request.UserId && a.DeviceNumber == request.DeviceNumber, a => a.Licenses).FirstOrDefault();

            if (device is not null)
            {
                var sortingLicense = device.Licenses.OrderByDescending(a => a.StartLicense.Ticks).ToList();
                var lastLicense = sortingLicense.FirstOrDefault();
                if (lastLicense != null)
                {
                    lastLicense = _licenseRepository.GetWithInclude(a => a.Id == lastLicense.Id, a => a.LicenseType).First();

                    var endLastlicense = lastLicense.StartLicense.AddDays(lastLicense.LicenseType.Duration);

                    var startLicense = endLastlicense >= DateTime.Now ? endLastlicense : DateTime.Now;

                    newLicense.StartLicense = startLicense;

                    device.Licenses.Add(newLicense);
                }
                else
                {
                    device.Licenses.Add(newLicense);
                }

                await _deviceRepository.UpdateAsync(device);
            }
            else
            {
                device = new Device() { DeviceNumber = request.DeviceNumber, UserId = request.UserId, LicenseKey = Guid.NewGuid().ToString() };
                device.Licenses.Add(newLicense);

                await _deviceRepository.CreateAsync(device);
            }

            return new CreateLicenseResponse() { Success = true };
        }
    }
}
