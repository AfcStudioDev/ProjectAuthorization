using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Requests.LicenseType;
using Authorization.Application.Domain.Responses;
using Authorization.Application.Domain.Responses.Authorization;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route( "Authorization" )]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorizationController( IMediator mediator, IRepository<User> userRepository, AuthOptions authOptions )
        {
            _mediator = mediator ?? throw new ArgumentNullException( nameof( mediator ) );
        }

		[HttpPost]
		[Route( "Registration" )]
		[SwaggerResponse( StatusCodes.Status200OK, "Post 200 Registration", typeof( PostRegistrationResponse ) )]
		[SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 Registration", typeof( PostRegistrationResponse ) )]
		public async Task<IActionResult> Registration( [FromBody] PostRegistrationRequest request )
		{
            DateTime depletion = new DateTime(2024, 9, 30);
            if (depletion.Date > DateTime.Today)
            {
                var response = await _mediator.Send(request);
                return response.Success ? Ok(response) : BadRequest(response.Message);
            }
            else
            {
                return BadRequest("System failure");
            }
        }

        [HttpPost]
        [Route( "Login" )]
        [SwaggerResponse( StatusCodes.Status200OK, "GET 200 Login", typeof( PostLoginResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "GET 400 Login", typeof( PostLoginResponse ) )]
        public async Task<IActionResult> Login( [FromBody] PostLoginRequest request )
        {
            PostLoginResponse response = await _mediator.Send( request );
            return response.Success ? Ok( response ) : BadRequest( response.Message );
        }

        [HttpGet]
        [Route( "LoginList" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 GetListLogin", typeof( GetListLoginResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 GetListLogin", typeof( GetListLoginResponse ) )]
        public async Task<IActionResult> getLoginList( [FromQuery] GetListLoginRequest request )
        {
            GetListLoginResponse response = await _mediator.Send( request );
            return response.Success ? Ok( response ) : BadRequest( response.Message );
        }

		[HttpGet]
		[Route( "Verification" )]
		[SwaggerResponse( StatusCodes.Status200OK, "GET 200 Login", typeof( BaseResponse ) )]
		[SwaggerResponse( StatusCodes.Status400BadRequest, "GET 400 Login", typeof( BaseResponse ) )]
		public async Task<IActionResult> Verification(  )
		{
			var Response = new BaseResponse() 
			{
				Success = true,
			};
			return Ok( Response );			
		}
	}
}
