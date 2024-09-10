using Authorization.Application.Domain.Responses.License;
using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class CreateLicenseRequest : IRequest<CreateLicenseResponse>
    {
        public Guid UserId { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public Guid LicenseType { get; set; }
    }
}
