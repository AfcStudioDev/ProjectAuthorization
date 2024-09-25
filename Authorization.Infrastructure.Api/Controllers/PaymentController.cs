using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;

using MediatR;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route( "payment" )]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController( IMediator mediator )
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route( "CreatePayment" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 Payment", typeof( CreatePaymentResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 Payment", typeof( CreatePaymentResponse ) )]
        public async Task<IActionResult> CreatePayment( [FromBody] CreatePaymentRequest request )
        {
            request.UserId = GetUserIdFromToken();

            CreatePaymentResponse response = await _mediator.Send( request );

            return response.Success ? Ok( response ) : BadRequest( response );
        }

        [Authorize]
        [HttpPost]
        [Route( "MakePaymentAndConfirmLicenseCreate" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 Payment", typeof( MakePaymentAndConfirmResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 Payment", typeof( MakePaymentAndConfirmResponse ) )]
        public async Task<IActionResult> MakePaymentAndConfirmLicenseCreate( [FromBody] MakePaymentAndConfirmRequest request )
        {
            request.UserId = GetUserIdFromToken();

            MakePaymentAndConfirmResponse response = await _mediator.Send( request );

            return response.Success ? Ok( response ) : BadRequest( response );
        }

        private Guid GetUserIdFromToken()
        {
            return Guid.Parse( HttpContext.User.Claims.FirstOrDefault( claim => claim.Type == "UserId" )!.Value );
        }
    }
}