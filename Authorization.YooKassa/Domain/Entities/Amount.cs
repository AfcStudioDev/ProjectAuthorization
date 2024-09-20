using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class Amount
    {
        [JsonPropertyName( "value" )]
        public string Value { get; set; } = null!;

        [JsonPropertyName( "currency" )]
        public string Currency { get; set; } = null!;
    }
}
