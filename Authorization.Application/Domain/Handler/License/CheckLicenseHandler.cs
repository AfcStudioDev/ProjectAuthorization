using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CheckLicenseHandler : IRequestHandler<CheckLicenseRequest, CheckLicenseResponse>
    {
        private readonly IRepository<Entities.Device> _repository;

        public CheckLicenseHandler( IRepository<Entities.Device> repository )
        {
            _repository = repository;
        }

        public async Task<CheckLicenseResponse> Handle( CheckLicenseRequest request, CancellationToken cancellationToken )
        {
            CheckLicenseResponse response = new() { Success = false };

            Entities.Device? license = _repository.Get( a => a.DeviceNumber == request.DeviceNumber ).FirstOrDefault();

            if (license is not null)
            {
                response.Success = true;
                response.Message = $"Лицензия до {license.ExpirationLicense}";
            }

            return response;
        }
    }
}
