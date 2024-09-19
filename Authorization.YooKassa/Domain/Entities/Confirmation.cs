using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class Confirmation
    {
        [JsonPropertyName( "type" )]
        public string Type { get; set; } = null!;

        [JsonPropertyName( "confirmation_token" )]
        public string ConfirmationToken { get; set; } = null!;
    }
}
