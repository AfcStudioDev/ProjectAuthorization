using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Handler.License
{
    public class DeleteLicenseHandler : IRequestHandler<DeleteLicenseRequest, DeleteLicenseResponse>
    {
        private readonly IRepository<Device> _repository;

        public DeleteLicenseHandler(IRepository<Device> repository)
        {
            _repository = repository;
        }
        public async Task<DeleteLicenseResponse> Handle(DeleteLicenseRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteLicenseResponse();

            var device = _repository.Get(a => a.DeviceNumber == request.DeviceNumber).FirstOrDefault();
            if (device is not null)
            {
                await _repository.RemoveAsync(device);

                response.Success = true;
            }
            else
                response.Message = "Лицензия не найдена";

            return response;
        }
    }
}
