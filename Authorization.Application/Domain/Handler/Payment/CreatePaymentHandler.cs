using System.Globalization;

using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;
using Authorization.YooKassa;
using Authorization.YooKassa.Domain.Entities;
using System.Threading;
using System.Linq;
using MediatR;

using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Authorization.Application.Domain.Handler.Payment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly IRepository<Entities.LicenseType> _repository;
        private readonly string _urlPayments;
        private readonly string _shopId;
        private readonly string _secretKey;

        public CreatePaymentHandler( IConfiguration configuration, IRepository<Entities.LicenseType> repository )
        {
            _urlPayments = configuration["YooCassaService:UrlPayments"]!;
            _shopId = configuration["YooCassaService:ShopId"]!;
            _secretKey = configuration["YooCassaService:SecretKey"]!;
            _repository = repository;
        }

        public async Task<CreatePaymentResponse> Handle( CreatePaymentRequest request, CancellationToken cancellationToken )
        {
            CreatePaymentResponse response = new CreatePaymentResponse();

            Entities.LicenseType licenseType = await _repository.FindByIdAsync( request.LicenseType );
            if (licenseType != null)
            {
                PayResponse? youCassaResponse = await CreatePaynetAndGetResponse( request, licenseType );
                MakeResponse( youCassaResponse, ref response );

                response.Pay = youCassaResponse;
            }
            else
            {
                response.Success = false;
                response.Message = "Нет выбранной лицензии";
            }

            return response;
        }

        private async Task<PayResponse?> CreatePaynetAndGetResponse( CreatePaymentRequest paymentRequest, Entities.LicenseType licenseType )
        {
            YooCassaClient youcassaClient = new YooCassaClient( _urlPayments, _shopId, _secretKey );

            PayResponse? response = await youcassaClient.CreateNewPay( new PayRequest()
            {
                Amount = new Amount() { Currency = "RUB", Value = $"{licenseType.Price.ToString( CultureInfo.InvariantCulture )}" },
                Capture = true,
                Description = $"Оплата за подписку для пользователя. Тип подписки {licenseType.Name}. Пользователь ID - { paymentRequest.UserId }",
                Confirmation = new Confirmation() { Type = "embedded" }
            } );

            return response;
        }

        /// <summary>
        /// Основываясь на состоянии платежа в юкассе, возвращает http код 200 для всех состояний кроме canceled
        /// </summary>
        private void MakeResponse( PayResponse? youCassaResponse, ref CreatePaymentResponse response )
        {
            if (youCassaResponse != null && youCassaResponse.Status != "canceled")
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
        }
    }
}
