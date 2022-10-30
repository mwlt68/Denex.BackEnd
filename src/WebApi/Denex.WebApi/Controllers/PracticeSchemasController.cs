using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaDelete;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonDelete;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert;
using Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate;
using Denex.Application.Features.Queries.PracticeSchemaList;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PracticeSchemasController : ControllerBase
    {
        private readonly IMediator mediator;
        public PracticeSchemasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PracticeSchema>>>> AllAsync()
        {
            var result = await mediator.Send(new PracticeSchemaListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<PracticeSchema>>> InsertAsync(PracticeSchemaInsertCommand insertCommand)
        {
            var result = await mediator.Send(insertCommand);
            return Created(nameof(InsertAsync),result);
        }

        [HttpPatch]
        public async Task<ActionResult<ServiceResponse<PracticeSchema>>> UpdateAsync(PracticeSchemaUpdateCommand  updateCommand)
        {
            var result = await mediator.Send(updateCommand);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var deleteCommand = new PracticeSchemaDeleteCommand() { Id = id};
            var result = await mediator.Send(deleteCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> LessonInsertAsync(PracticeSchemaLessonInsertCommand insertCommand)
        {
            var result = await mediator.Send(insertCommand);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> LessonDeleteAsync(string id)
        {
            var deleteCommand = new PracticeSchemaLessonDeleteCommand() { Id = id };
            var result = await mediator.Send(deleteCommand);
            return Ok(result);
        }
    }
}

