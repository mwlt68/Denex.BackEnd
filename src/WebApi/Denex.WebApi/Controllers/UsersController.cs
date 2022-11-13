using Denex.Application.Dto;
using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Application.Features.Queries.UserAuthentication;
using Denex.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        /// <summary>
        /// Return token for project authentication.
        /// </summary>
        /// <remarks>
        /// To access some endpoints is to get token with username and password.
        /// </remarks>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ServiceResponse<UserAuthenticationDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(VoidServiceResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(VoidServiceResponse))]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserAuthenticationDto>>> AuthenticationAsync([FromBody] UserAuthenticationQuery userAuthentication)
        {
            var result = await mediator.Send(userAuthentication);
            return Ok(result);
        }

        /// <summary>
        /// Create a User based on the requested data.
        /// </summary>
        /// <remarks>
        /// Return authentication token of the created user.
        /// </remarks>
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(ServiceResponse<UserAuthenticationDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(VoidServiceResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(VoidServiceResponse))]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserAuthenticationDto>>> InsertAsync([FromBody] UserInsertCommand userInsertCommand)
        {
            var result = await mediator.Send(userInsertCommand);
            return StatusCode(201,result);
        }
    }
}
