using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class DeleteLicenseRequest : IRequest<DeleteLicenseResponse>
    {
        public string DeviceNumber { get; set; } = null!;
    }
}
