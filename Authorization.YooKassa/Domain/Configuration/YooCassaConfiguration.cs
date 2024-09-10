namespace Authorization.YooKassa.Domain.Configuration
{
    public class YooCassaConfiguration
    {
        public string UrlPayments { get; set; } = null!;
        public string ShopId { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
