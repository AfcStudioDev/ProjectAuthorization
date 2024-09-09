namespace Authorization.Application.Domain.Entities
{
    public class LicenseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Decimal Price { get; set; }
        public TimeSpan Length { get; set; }

        public List<License> Licenses { get; set; } = new();
    }
}
