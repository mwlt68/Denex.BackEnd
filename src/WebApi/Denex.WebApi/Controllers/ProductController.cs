using Denex.Application.Dto;
using Denex.Application.Features.Commands.CreateProduct;
using Denex.Application.Features.Queries.GetAllProduct;
using Denex.Application.Features.Queries.GetProductById;
using Denex.Application.Repository;
using Denex.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllProductQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductCommand createProductCommand)
        {
            var result = await mediator.Send(createProductCommand);
            return Ok(result);
        }
    }
}
