using Authorization.YooKassa.Domain.Entities;

namespace Authorization.Application.Domain.Responses.Payment
{
    public class CreatePaymentResponse : BaseResponse
    {
        public PayResponse? Pay { get; set; }
    }
}