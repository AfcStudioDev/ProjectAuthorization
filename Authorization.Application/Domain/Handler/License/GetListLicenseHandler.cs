using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class GetListLicenseHandler : IRequestHandler<GetListLicenseRequest, GetListDeviceResponse>
    {
        private readonly IRepository<Entities.User> _repository;

        public GetListLicenseHandler( IRepository<Entities.User> repository )
        {
            _repository = repository;
        }

        public async Task<GetListDeviceResponse> Handle( GetListLicenseRequest request, CancellationToken cancellationToken )
        {
            List<Entities.Device> response = _repository.GetWithInclude( a => a.Id == request.UserId, a => a.Devices ).First().Devices;

            return new GetListDeviceResponse() { Devices = response, Success = true };
        }
    }
}