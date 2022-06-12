using Denex.Application.Dto;
using Denex.Application.Repository;
using Denex.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denex.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> products= await productRepository.GetAll();
            var productDtos = products.Select(p => new ProductViewDto(p.Id, p.Name));
            return Ok(productDtos);
        }
    }
}
