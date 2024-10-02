using System.Text.Json.Serialization;

namespace Authorization.YooKassa.Domain.Entities
{
    public class Receipt
    {

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; } = null!;

        [JsonPropertyName("items")]
        public List<ItemStore> Items { get; set; } = new();
    }
}
