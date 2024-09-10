using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class Recipient
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = null!;

        [JsonPropertyName("gateway_id")]
        public string GatewayId { get; set; } = null!;
    }
}
