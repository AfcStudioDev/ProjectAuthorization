using Authorization.Application.Domain.Responses.Payment;

using MediatR;

namespace Authorization.Application.Domain.Requests.Payment
{
    public class MakePaymentAndConfirmRequest : IRequest<MakePaymentAndConfirmResponse>
    {
        public uint? UserId { get; set; }
        public string PaymentId { get; set; } = null!;
        public uint LicenseType { get; set; }
        public string DeviceNumber { get; set; } = null!;
    }
}