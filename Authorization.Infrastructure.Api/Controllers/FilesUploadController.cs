using Authorization.Application.Domain.Requests.FilesUpload;
using Authorization.Application.Domain.Responses.FilesUpload;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route("Files")]
    public class FilesUploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 getDistr", typeof(GetFilesUploadResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post getDistr", typeof(GetFilesUploadResponse))]
        public async Task<IActionResult> GetDistr(GetFilesUploadRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return response.File!;
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
