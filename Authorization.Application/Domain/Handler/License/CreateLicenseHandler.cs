using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CreateLicenseHandler : IRequestHandler<CreateLicenseRequest, CreateLicenseResponse>
    {
        private readonly IRepository<Entities.Device> _licenseRepository;
        private readonly IRepository<Entities.LicenseType> _licenseTypeRepository;
        private readonly IRepository<Entities.User> _userRepository;

        public CreateLicenseHandler( IRepository<Entities.Device> entitiesRepository, IRepository<Entities.LicenseType> licenseTypeRepository, IRepository<Entities.User> userRepository )
        {
            _licenseRepository = entitiesRepository;
            _licenseTypeRepository = licenseTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateLicenseResponse> Handle( CreateLicenseRequest request, CancellationToken cancellationToken )
        {
            var currentDate = DateOnly.FromDateTime( DateTime.Now.Date );
            Entities.Device newLicense = new Entities.Device() { ExpirationLicense = currentDate };
            Entities.Device? device = _licenseRepository.GetWithInclude( a => a.UserId == request.UserId && a.DeviceNumber == request.DeviceNumber ).FirstOrDefault();

            Entities.LicenseType licenseType = await _licenseTypeRepository.FindByIdAsync( (uint)request.LicenseType );

            if (device is not null)
            {
                if (device.ExpirationLicense >= currentDate )
                {
                    device.ExpirationLicense = device.ExpirationLicense.AddDays( licenseType.Duration );
                }
                else
                {
                    device.ExpirationLicense = currentDate.AddDays( licenseType.Duration );
                }

                _ = await _licenseRepository.UpdateAsync( device );
            }
            else
            {
                Entities.User user = await _userRepository.FindByIdAsync( request.UserId );

                device = new Entities.Device() { DeviceNumber = request.DeviceNumber, UserId = request.UserId, ExpirationLicense = currentDate.AddDays( licenseType.Duration ) };

                user.Devices.Add( device );

                _ = await _userRepository.UpdateAsync( user );
            }

            return new CreateLicenseResponse() { Success = true };
        }
    }
}