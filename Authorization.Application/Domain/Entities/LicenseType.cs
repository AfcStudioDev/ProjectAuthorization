using System.Text.Json.Serialization;

namespace Authorization.Application.Domain.Entities
{
    public class LicenseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
