using Authorization.Application.Domain.Responses.Payment;

using MediatR;

namespace Authorization.Application.Domain.Requests.Payment
{
    public class CreatePaymentRequest : IRequest<CreatePaymentResponse>
    {
        public uint? UserId { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public uint LicenseType { get; set; }
    }
}
