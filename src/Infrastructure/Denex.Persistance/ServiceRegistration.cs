using Denex.Application.Repository;
using Denex.Persistance.Context;
using Denex.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Denex.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<IProductRepository, ProductRespository>();
        }
    }
}
