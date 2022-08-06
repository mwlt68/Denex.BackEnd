using Denex.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Denex.Persistance.Service
{
    internal class HttpContextService: IHttpContextService
    {
        private readonly IHttpContextAccessor contextAccessor;
        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public string? GetUserIdFromClaims()
        {
            return contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
}
