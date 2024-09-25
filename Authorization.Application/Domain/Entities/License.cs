using System;
using System.Text.Json.Serialization;

namespace Authorization.Application.Domain.Entities
{
    public class License
    {
        public Guid Id { get; set; }
        public DateTime StartLicense { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public string LicenseKey { get; set; } = null!;

        public int Duration { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
