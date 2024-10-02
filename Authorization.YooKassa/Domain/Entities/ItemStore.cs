using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class ItemStore
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("amount")]
        public Amount Amount { get; set; } = null!;

        [JsonPropertyName("vat_code")]
        public int VatCode { get; set; } = 1;

        [JsonPropertyName("payment_mode")]
        public string PaymentMode { get; set; } = "full_prepayment";

        [JsonPropertyName("payment_subject")]
        public string PaymentSubject { get; set; } = "commodity";
    }
}
