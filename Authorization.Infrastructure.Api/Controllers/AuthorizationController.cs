using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Infrastructure.Api.Controllers
{
	[Route( "Authorization" )]
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
			var response = await _mediator.Send( request );

			if ( response.Success )
			{
				return Ok( response );
			}
			else
			{
				return BadRequest( response );
			}
		}

		[HttpPost]
		[Route( "Login" )]
		[SwaggerResponse( StatusCodes.Status200OK, "GET 200 Login", typeof( PostLoginResponse ) )]
		[SwaggerResponse( StatusCodes.Status400BadRequest, "GET 400 Login", typeof( PostLoginResponse ) )]
		public async Task<IActionResult> Login( [FromBody] PostLoginRequest request )
		{
			var response = await _mediator.Send( request );

			if ( response.Success )
			{
				return Ok( response );
			}
			else
			{
				return BadRequest( response );
			}
		}
	}
}
