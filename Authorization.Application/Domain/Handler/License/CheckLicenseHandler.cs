using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class CheckLicenseHandler : IRequestHandler<CheckLicenseRequest, CheckLicenseResponse>
    {
        private readonly IRepository<Entities.License> _repository;

        public CheckLicenseHandler( IRepository<Entities.License> repository )
        {
            _repository = repository;
        }

        public async Task<CheckLicenseResponse> Handle( CheckLicenseRequest request, CancellationToken cancellationToken )
        {
            CheckLicenseResponse response = new() { Success = false };

            Entities.License? license = _repository.Get( a => a.DeviceNumber == request.DeviceNumber ).FirstOrDefault();

            if (license is not null)
            {
                response.Success = true;
                response.Message = $"Лицензия до {license.StartLicense.AddDays( license.Duration ).Date}";
            }

            return response;
        }
    }
}
