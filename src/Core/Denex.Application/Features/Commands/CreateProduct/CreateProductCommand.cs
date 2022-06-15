using AutoMapper;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ServiceResponse<Product>>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Product>>
        {
            IProductRepository productRepository;
            IMapper mapper;
            public CreateProductCommandHandler(IMapper mapper,IProductRepository productRepository)
            {
                this.mapper = mapper;
                this.productRepository = productRepository;
            }
            public async Task<ServiceResponse<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var productModel= mapper.Map<Product>(request);
                var product = await productRepository.Add(productModel);
                return new ServiceResponse<Product>(product);
            }
        }
    }
}
