﻿using Authorization.Application.Domain.Requests.LicenseType;
using Authorization.Application.Domain.Responses.License;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route("LicenseType")]
    public class LicenseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicenseTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 GetListTypeLicense", typeof(GetListLicenseResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 GetListTypeLicense", typeof(GetListLicenseResponse))]
        public async Task<IActionResult> getAll(GetListLicenseTypeRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
