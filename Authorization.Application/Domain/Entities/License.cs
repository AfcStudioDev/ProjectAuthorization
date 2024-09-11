using System.Text.Json.Serialization;

namespace Authorization.Application.Domain.Entities
{
    public class License
    {
        public Guid Id { get; set; }
        public DateTime StartLicense { get; set; }

        public Guid DeviceId { get; set; }

        [JsonIgnore]
        public Device Device { get; set; } = null!;
        public Guid LicenseTypeId { get; set; }
        public LicenseType LicenseType { get; set; } = null!;
    }
}
