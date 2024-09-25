using Authorization.Application.Domain.Responses.Payment;

using MediatR;
using System;

namespace Authorization.Application.Domain.Requests.Payment
{
    public class CreatePaymentRequest : IRequest<CreatePaymentResponse>
    {
        public Guid? UserId { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public Guid LicenseType { get; set; }
    }
}
