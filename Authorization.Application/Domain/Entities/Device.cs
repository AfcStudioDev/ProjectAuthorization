namespace Authorization.Application.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string DeviceNumber { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<License> Licenses { get; set; } = new();
    }
}
