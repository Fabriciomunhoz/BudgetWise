using HelpFinance.Models.DTOs.Usuario;

namespace HelpFinance.Middleware
{
    public interface IAuthorizationMiddleware
    {
        public Task<TokenResponse?> LoginAsync(string login, string senha);
    }
}
