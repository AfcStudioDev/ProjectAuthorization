using Authorization.Application.Domain.Requests.User;
using Authorization.Application.Domain.Responses.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.Infrastructure.Api.Controllers
{
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 CreateUserResponse", typeof(CreateUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 CreateUserResponse", typeof(CreateUserResponse))]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
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

        [Authorize]
        [HttpPut]
        [Route("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 UpdateUser", typeof(UpdateUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 UpdateUser", typeof(UpdateUserResponse))]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
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

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 UpdateUser", typeof(UpdateUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 UpdateUser", typeof(UpdateUserResponse))]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
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

        [Authorize]
        [HttpGet]
        [Route("Get")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 GetUser", typeof(GetUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 GetUser", typeof(GetUserResponse))]
        public async Task<IActionResult> GetUser(GetUserRequest request)
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

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        [SwaggerResponse(StatusCodes.Status200OK, "Post 200 GetListUserResponse", typeof(GetListUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Post 400 GetListUserResponse", typeof(GetListUserResponse))]
        public async Task<IActionResult> GetAllUser(GetListUserRequest request)
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
