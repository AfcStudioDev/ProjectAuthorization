﻿using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CreateLicenseHandler : IRequestHandler<CreateLicenseRequest, CreateLicenseResponse>
    {
        private readonly IRepository<Entities.License> _licenseRepository;
        private readonly IRepository<Entities.LicenseType> _licenseTypeRepository;
        private readonly IRepository<Entities.User> _userRepository;

        public CreateLicenseHandler(IRepository<Entities.License> entitiesRepository, IRepository<Entities.LicenseType> licenseTypeRepository, IRepository<Entities.User> userRepository)
        {
            _licenseRepository = entitiesRepository;
            _licenseTypeRepository = licenseTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateLicenseResponse> Handle(CreateLicenseRequest request, CancellationToken cancellationToken)
        {
            var newLicense = new Entities.License() { StartLicense = DateTime.Now };
            var license = _licenseRepository.GetWithInclude(a => a.UserId == request.UserId && a.DeviceNumber == request.DeviceNumber).FirstOrDefault();

            var licenseType = await _licenseTypeRepository.FindByIdAsync(request.LicenseType);

            if (license is not null)
            {
                if(license.StartLicense.AddDays(license.Duration) >= DateTime.Now)
                    license.Duration += licenseType.Duration;
                else
                {
                    license.StartLicense = DateTime.Now;
                    license.Duration = licenseType.Duration;
                }

                await _licenseRepository.UpdateAsync(license);
            }
            else
            {
                var user = await _userRepository.FindByIdAsync(request.UserId);

                license = new Entities.License() { DeviceNumber = request.DeviceNumber, UserId = request.UserId, LicenseKey = Guid.NewGuid().ToString(), Duration = licenseType.Duration, StartLicense = DateTime.Now };

                user.Licenses.Add(license);

                await _userRepository.UpdateAsync(user);
            }

            return new CreateLicenseResponse() { Success = true };
        }
    }
}
