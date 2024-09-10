namespace Authorization.Application.Domain.Entities
{
    public class License
    {
        public Guid Id { get; set; }
        public DateTime StartLicense { get; set; }

        public List<Device> Devices { get; set; } = new();
        public Guid LicenseTypeId { get; set; }
        public LicenseType LicenseType { get; set; } = null!;
    }
}
