using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Features.Commands.CreateProduct;
using Denex.Application.Features.Queries.GetProductById;
using Denex.Domain.Entities;

namespace Denex.Application.Mapping
{
    internal class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product,ProductViewDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, GetProductByIdViewModel>().ReverseMap();
        }
    }
}
