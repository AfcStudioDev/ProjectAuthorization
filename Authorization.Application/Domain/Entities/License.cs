namespace Authorization.Application.Domain.Entities
{
    public class License
    {
        public Guid Id { get; set; }
        public string LicenseKey { get; set; } = null!;
        public DateTime StartLicense { get; set; }

        public List<Device> Devices { get; set; } = new();
        public int LicenseTypeId { get; set; }
        public LicenseType LicenseType { get; set; } = null!;
    }
}
