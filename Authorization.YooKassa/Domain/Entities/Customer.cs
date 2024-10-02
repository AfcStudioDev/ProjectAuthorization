using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class Customer
    {

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}
