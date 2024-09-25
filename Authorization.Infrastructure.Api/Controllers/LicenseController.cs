using Authorization.Application.Domain.Requests.License;
using Authorization.Application.Domain.Responses.License;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route( "License" )]
    public class LicenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicenseController( IMediator mediator )
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route( "all" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 GetAllLicense", typeof( GetListLicenseResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 GetAllLicense", typeof( GetListLicenseResponse ) )]
        public async Task<IActionResult> GetAllLicense( GetListLicenseRequest request )
        {
            request.UserId = GetUserIdFromToken();

            GetListLicenseResponse response = await _mediator.Send( request );

            //if (response.Success)
            //{
            //    return Ok( response );
            //}
            //else
            //{
            //    return BadRequest( response );
            //}

            return response.Success ? Ok( response ) : BadRequest( response );
        }

        [HttpGet]
        [Route( "check" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 CheckLicense", typeof( CheckLicenseResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 CheckLicense", typeof( CheckLicenseResponse ) )]
        public async Task<IActionResult> Check( CheckLicenseRequest request )
        {
            CheckLicenseResponse response = await _mediator.Send( request );

            //if (response.Success)
            //{
            //    return Ok( response );
            //}
            //else
            //{
            //    return BadRequest( response );
            //}
            return response.Success ? Ok( response ) : BadRequest( response );
        }

        [Authorize]
        [HttpPost]
        [Route( "Create" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 CreateLicense", typeof( CreateLicenseResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 CreateLicense", typeof( CreateLicenseResponse ) )]
        public async Task<IActionResult> Create( CreateLicenseRequest request )
        {
            request.UserId = GetUserIdFromToken();

            CreateLicenseResponse response = await _mediator.Send( request );

            //if (response.Success)
            //{
            //    return Ok( response );
            //}
            //else
            //{
            //    return BadRequest( response );
            //}
            return response.Success ? Ok( response ) : BadRequest( response );
        }

        [Authorize]
        [HttpPost]
        [Route( "Delete" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 DeleteLicense", typeof( DeleteLicenseResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 DeleteLicense", typeof( DeleteLicenseResponse ) )]
        public async Task<IActionResult> Delete( DeleteLicenseRequest request )
        {
            DeleteLicenseResponse response = await _mediator.Send( request );

            //if (response.Success)
            //{
            //    return Ok( response );
            //}
            //else
            //{
            //    return BadRequest( response );
            //}
            return response.Success ? Ok( response ) : BadRequest( response );
        }

        private Guid GetUserIdFromToken()
        {
            return Guid.Parse( HttpContext.User.Claims.FirstOrDefault( claim => claim.Type == "UserId" )!.Value );
        }
    }
}
