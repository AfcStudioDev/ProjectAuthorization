using System.Text.Json.Serialization;
using System;

namespace Authorization.YooKassa.Domain.Entities
{
    public class PayResponse
    {
        [JsonPropertyName( "id" )]
        public string Id { get; set; } = null!;

        [JsonPropertyName( "status" )]
        public string Status { get; set; } = null!;

        [JsonPropertyName( "paid" )]
        public bool Paid { get; set; }

        [JsonPropertyName( "amount" )]
        public Amount Amount { get; set; } = null!;

        [JsonPropertyName( "confirmation" )]
        public Confirmation Confirmation { get; set; } = null!;

        [JsonPropertyName( "created_at" )]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName( "description" )]
        public string Description { get; set; } = null!;

        [JsonPropertyName( "metadata" )]
        public Metadata Metadata { get; set; } = null!;

        [JsonPropertyName( "recipient" )]
        public Recipient Recipient { get; set; } = null!;

        [JsonPropertyName( "refundable" )]
        public bool Refundable { get; set; }

        [JsonPropertyName( "test" )]
        public bool Test { get; set; }
    }
}
