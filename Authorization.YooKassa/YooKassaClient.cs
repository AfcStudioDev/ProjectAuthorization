using Authorization.YooKassa.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Authorization.YooKassa
{
    public class YooCassaClient
    {
        private readonly string UrlPayments;
        private readonly string ShopId;
        private readonly string SecretKey;
        private HttpClient _httpClient;

        public YooCassaClient(string urlPayments, string shopId, string secretkey)
        {
            UrlPayments = urlPayments;
            ShopId = shopId;
            SecretKey = secretkey;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(UrlPayments);
            _httpClient.DefaultRequestHeaders.Add("Authorization",
                "Basic " +
                Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(ShopId + ":" + SecretKey)));
        }

        public async Task<PayResponse?> CreateNewPay(PayRequest request)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/v3/payments");
            message.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());
            message.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PayResponse>(text)!;
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<bool> IsSuccessPay(string id)
        {
            bool successFlag = false;

            var message = new HttpRequestMessage(HttpMethod.Get, "/v3/payments/" + id);
            message.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());

            var response = await _httpClient.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<PayResponse>(text)!;
                successFlag = result.Id == id && result.Status == "succeeded";
            }
            else
            {
                var text = await response.Content.ReadAsStringAsync();
            }

            return successFlag;
        }
    }
}
