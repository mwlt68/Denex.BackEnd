using Denex.Application.Exceptions;
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
        public string GetUserIdFromClaims()
        {
            var userId = contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (userId == null)
                throw new UserNotFoundException();
            else return userId;
        }
    }
}
