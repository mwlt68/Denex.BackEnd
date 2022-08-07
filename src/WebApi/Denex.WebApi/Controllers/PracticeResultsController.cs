using Denex.Application.Features.Commands.PracticeResults.PracticeResultInsert;
using Denex.Application.Features.Commands.PracticeResults.PracticeResultUpdate;
using Denex.Application.Features.Queries.PracticeResult.PracticeResultList;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await mediator.Send(new PracticeResultListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(PracticeResultInsertCommand practiceResultInsert)
        {
            var result = await mediator.Send(practiceResultInsert);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(PracticeResultUpdateCommand practiceResultUpdateCommand)
        {
            var result = await mediator.Send(practiceResultUpdateCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(PracticeResultUpdateCommand practiceResultUpdateCommand)
        {
            var result = await mediator.Send(practiceResultUpdateCommand);
            return Ok(result);
        }
    }
}
