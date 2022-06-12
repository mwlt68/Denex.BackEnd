using Denex.Application.Repository;
using Denex.Domain.Entities;
using Denex.Persistance.Context;

namespace Denex.Persistance.Repositories
{
    internal class ProductRespository : GenericRepository<Product>, IProductRepository
    {
        public ProductRespository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
