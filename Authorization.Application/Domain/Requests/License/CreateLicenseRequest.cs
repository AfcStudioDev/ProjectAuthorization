using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class CreateLicenseRequest : IRequest<CreateLicenseResponse>
    {
        public uint UserId { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public uint LicenseType { get; set; }
    }
}
