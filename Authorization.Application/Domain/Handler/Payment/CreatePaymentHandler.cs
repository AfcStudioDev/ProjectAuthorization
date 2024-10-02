using System.Globalization;

using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;
using Authorization.YooKassa;
using Authorization.YooKassa.Domain.Entities;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Domain.Handler.Payment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly IRepository<Entities.LicenseType> _repository;
        private readonly IRepository<Entities.User> _userRepository;
        private readonly string _urlPayments;
        private readonly string _shopId;
        private readonly string _secretKey;

        public CreatePaymentHandler( IConfiguration configuration, IRepository<Entities.LicenseType> repository, IRepository<Entities.User> userRepository )
        {
            _urlPayments = configuration["YooCassaService:UrlPayments"]!;
            _shopId = configuration["YooCassaService:ShopId"]!;
            _secretKey = configuration["YooCassaService:SecretKey"]!;
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<CreatePaymentResponse> Handle( CreatePaymentRequest request, CancellationToken cancellationToken )
        {
            CreatePaymentResponse response = new CreatePaymentResponse();

            Entities.LicenseType licenseType = await _repository.FindByIdAsync( (uint)request.LicenseType );
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
            var user = _userRepository.FindById((uint)paymentRequest.UserId!);

            YooCassaClient youcassaClient = new YooCassaClient( _urlPayments, _shopId, _secretKey );
            var amount = new Amount() { Currency = "RUB", Value = $"{licenseType.Price.ToString(CultureInfo.InvariantCulture)}" };
            PayResponse? response = await youcassaClient.CreateNewPay( new PayRequest()
            {
                Amount = amount,
                Capture = true,
                Description = $"Оплата за подписку для пользователя. Тип подписки {licenseType.Name}. Пользователь ID - { paymentRequest.UserId }",
                Confirmation = new Confirmation() { Type = "embedded" },
                Receipt = new Receipt() { Customer = new Customer() { Email = user.Email }, Items = new() { new ItemStore() { Amount = amount, Description = licenseType.Name, Quantity = 1 } } }
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
