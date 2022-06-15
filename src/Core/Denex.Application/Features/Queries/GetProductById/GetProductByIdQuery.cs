using AutoMapper;
using Denex.Application.Repository;
using Denex.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ServiceResponse<GetProductByIdViewModel>>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductByIdViewModel>>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper mapper;
            public GetProductByIdQueryHandler(IMapper mapper,IProductRepository productRepository)
            {
                this.mapper = mapper;
                this.productRepository = productRepository;
            }
            public async Task<ServiceResponse<GetProductByIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await productRepository.GetById(request.Id);
                var productViewModel=mapper.Map<GetProductByIdViewModel>(product);
                return new ServiceResponse<GetProductByIdViewModel>(productViewModel);
            }
        }
    }
}
