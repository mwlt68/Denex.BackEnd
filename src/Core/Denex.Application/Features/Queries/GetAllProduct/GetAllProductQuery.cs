using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQuery:IRequest<ServiceResponse<List<ProductViewDto>>>
    {

        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ServiceResponse<List<ProductViewDto>>>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper mapper;
            public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                this.productRepository = productRepository;
                this.mapper = mapper;
            }
            public async Task<ServiceResponse<List<ProductViewDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var products = await productRepository.GetAll();
                var productViewDtos=mapper.Map<List<ProductViewDto>> (products);
                return new ServiceResponse<List<ProductViewDto>>(productViewDtos);
            }
        }
    }
}
