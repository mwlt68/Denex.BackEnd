using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Denex.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly= Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
        }
    }
}
