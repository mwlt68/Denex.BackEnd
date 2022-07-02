using Denex.Application.Features.Commands.Users.UserInsert;
using Denex.Application.Features.Queries.UserAuthentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public IActionResult CheckServer()
        {
            return Ok("Service is running");
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(UserAuthenticationQuery userAuthentication)
        {
            var result = await mediator.Send(userAuthentication);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(UserInsertCommand userInsertCommand)
        {
            var result = await mediator.Send(userInsertCommand);
            return Ok(result);
        }
    }
}
