using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;
using Authorization.YooKassa;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Domain.Handler.Payment
{
    public class MakePaymentAndConfirmHandler : IRequestHandler<MakePaymentAndConfirmRequest, MakePaymentAndConfirmResponse>
    {
        private readonly IMediator _mediator;
        private readonly string _urlPayments;
        private readonly string _shopId;
        private readonly string _secretKey;

        public MakePaymentAndConfirmHandler( IMediator mediator, IConfiguration configuration )
        {
            _mediator = mediator;
            _urlPayments = configuration["YooCassaService:UrlPayments"]!;
            _shopId = configuration["YooCassaService:ShopId"]!;
            _secretKey = configuration["YooCassaService:SecretKey"]!;
        }

        public async Task<MakePaymentAndConfirmResponse> Handle( MakePaymentAndConfirmRequest request, CancellationToken cancellationToken )
        {
            MakePaymentAndConfirmResponse response = new MakePaymentAndConfirmResponse();

            if (await ConfirmYoucassaPayment( request.PaymentId ))
            {
                CreateLicenseRequest createLicenseRequest = new CreateLicenseRequest() { UserId = (Guid)request.UserId!, DeviceNumber = request.DeviceNumber, LicenseType = request.LicenseType };
                Responses.License.CreateLicenseResponse responseCreateLicense = await _mediator.Send( createLicenseRequest );

                if (responseCreateLicense.Success)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Не удалось создать лицензию";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Не удалось подтвердить платеж";
            }

            return response;
        }

        /// <summary>
        /// Отправка запроса в Юкассу для подтверждения успешного платежа
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        private async Task<bool> ConfirmYoucassaPayment( string paymentId )
        {
            YooCassaClient client = new YooCassaClient( _urlPayments, _shopId, _secretKey );
            bool response = await client.IsSuccessPay( paymentId );

            return response;
        }
    }
}
