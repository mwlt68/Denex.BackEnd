using Denex.Application.Features.Commands.PracticeResults.PracticeResultInsert;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PracticeResultsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public PracticeResultsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(PracticeResultInsertCommand practiceResultInsert)
        {
            var result = await mediator.Send(practiceResultInsert);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(PracticeResultInsertCommand practiceResultInsert)
        {
            var result = await mediator.Send(practiceResultInsert);
            return Ok(result);
        }
    }
}
