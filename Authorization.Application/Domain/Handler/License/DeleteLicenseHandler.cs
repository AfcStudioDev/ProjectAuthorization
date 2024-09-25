using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using System.Threading.Tasks;

using System.Threading;
using MediatR;
using System.Linq;

namespace Authorization.Application.Domain.Handler.License
{
    public class DeleteLicenseHandler : IRequestHandler<DeleteLicenseRequest, DeleteLicenseResponse>
    {
        private readonly IRepository<Entities.License> _repository;

        public DeleteLicenseHandler( IRepository<Entities.License> repository )
        {
            _repository = repository;
        }
        public async Task<DeleteLicenseResponse> Handle( DeleteLicenseRequest request, CancellationToken cancellationToken )
        {
            DeleteLicenseResponse response = new DeleteLicenseResponse();

            Entities.License? license = _repository.Get( a => a.DeviceNumber == request.DeviceNumber ).FirstOrDefault();
            if (license is not null)
            {
                _ = await _repository.RemoveAsync( license );

                response.Success = true;
            }
            else
            {
                response.Message = "Лицензия не найдена";
            }

            return response;
        }
    }
}
