using Denex.Application.Interfaces.Service;
using Denex.Application.Repository;
using Denex.Persistance.Extensions;
using Denex.Persistance.Repositories;
using Denex.Persistance.Service;
using Denex.Persistance.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Denex.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region JWT
            
            services.Configure<JwtSettings>(settings=>
            {
                configuration.GetSection("JwtSettings").Bind(settings);
            });
            string jwtKey=configuration.GetValue<string>("JwtSettings:Key");
            services.AddCustomJwtToken(jwtKey);
            services.AddSingleton<IJwtService, JwtService>();
            #endregion
            services.AddCustomSwaggerGen();
            services.Configure<MongoDbSettings>(settings =>
            {
                configuration.GetSection("MongoDbSettings").Bind(settings);
            });
 //           services.Configure<MongoDbSettings>(options => configuration.GetSection("MongoDbSettings"));
            services.AddTransient<IUserRepository, UserRespository>();
        }
    }
}
