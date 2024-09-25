using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class PayRequest
    {
        [JsonPropertyName( "amount" )]
        public Amount Amount { get; set; } = null!;

        [JsonPropertyName( "confirmation" )]
        public Confirmation Confirmation { get; set; } = null!;

        [JsonPropertyName( "capture" )]
        public bool Capture { get; set; }

        [JsonPropertyName( "description" )]
        public string Description { get; set; } = null!;
    }
}
