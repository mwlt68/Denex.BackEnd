using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Denex.Persistance.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Denex.API",Description = "A tutorial project to provide documentation for our existing APIs.",Contact= new OpenApiContact() {Name="Mevlüt Gür",Email="mwltgr@gmail.com" }, Version = "v1" });
                var xmlFile = $"denex.webapi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // JWT Bearer Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    }
                );
                c.ExampleFilters();
                // It check  whether it is or not of Authorize annotation in every controller (Denex.WebApi/Controller) 
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });
            // It scan Application (which i set in Program.cs under the Denex.WebApi) assembly  to find validators. (Denex.Application/Features/**Validator)
            services.AddFluentValidationRulesToSwagger();
            // It scan Persistance(executing) assembly for endpoint request body model examples. (Denex.Persistance/Examples)
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
        }
    }
}
