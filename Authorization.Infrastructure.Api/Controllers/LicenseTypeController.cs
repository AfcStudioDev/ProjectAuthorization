using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Responses.License;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route( "LicenseType" )]
    public class LicenseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicenseTypeController( IMediator mediator )
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        [Route( "" )]
        [SwaggerResponse( StatusCodes.Status200OK, "Post 200 GetListTypeLicense", typeof( GetListLicenseResponse ) )]
        [SwaggerResponse( StatusCodes.Status400BadRequest, "Post 400 GetListTypeLicense", typeof( GetListLicenseResponse ) )]
        public async Task<IActionResult> getAll( GetListLicenseTypeRequest request )
        {
            Application.Domain.Responses.LicenseType.GetListLicenseTypeResponse response = await _mediator.Send( request );

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
    }
}
