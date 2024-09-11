using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Authorization.YooKassa.Domain.Entities;
using Authorization.YooKassa;
using Authorization.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Application.Domain.Handler.Payment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly IRepository<Entities.LicenseType> _repository;
        private readonly string _urlPayments;
        private readonly string _shopId;
        private readonly string _secretKey;

        public CreatePaymentHandler(IConfiguration configuration, IRepository<Entities.LicenseType> repository)
        {
            _urlPayments = configuration["YooCassaService:UrlPayments"]!;
            _shopId = configuration["YooCassaService:ShopId"]!;
            _secretKey = configuration["YooCassaService:SecretKey"]!;
            _repository = repository;
        }

        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var response = new CreatePaymentResponse();

            var licenseType = await _repository.FindByIdAsync(request.LicenseType);
            if (licenseType != null)
            {
                PayResponse? youCassaResponse = await CreatePaynetAndGetResponse(request, licenseType);
                MakeResponse(youCassaResponse, ref response);
            }
            else
            {
                response.Success = false;
                response.Message = "Нет выбранной лицензии";
            }

            return response;
        }

        private async Task<PayResponse?> CreatePaynetAndGetResponse(CreatePaymentRequest paymentRequest, Entities.LicenseType licenseType)
        {
            var youcassaClient = new YooCassaClient(_urlPayments, _shopId, _secretKey);

            var response = await youcassaClient.CreateNewPay(new PayRequest()
            {
                Amount = new Amount() { Currency = "RUB", Value = $"{licenseType.Price.ToString(CultureInfo.InvariantCulture)}" },
                Capture = true,
                Description = $"Оплата за подписку для пользователя. Тип подписки {licenseType.Name}",
                Confirmation = new Confirmation() { Type = "embedded" }
            });

            return response;
        }

        /// <summary>
        /// Основываясь на состоянии платежа в юкассе, возвращает http код 200 для всех состояний кроме canceled
        /// </summary>
        private void MakeResponse(PayResponse? youCassaResponse, ref CreatePaymentResponse response)
        {
            if (youCassaResponse != null && youCassaResponse.Status != "canceled")
                response.Success = true;
            else
               response.Success = false;    
        }
    }
}
