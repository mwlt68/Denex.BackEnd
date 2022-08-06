namespace Denex.Application.Interfaces.Service
{
    public interface IHttpContextService
    {
        public string? GetUserIdFromClaims();
    }
}
