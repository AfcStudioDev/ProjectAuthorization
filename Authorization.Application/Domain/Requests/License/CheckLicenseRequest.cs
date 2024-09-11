using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class CheckLicenseRequest : IRequest<CheckLicenseResponse>
    {
        public string DeviceNumber { get; set; } = null!;
        public string LicenseKey { get; set; } = null!;
    }
}
