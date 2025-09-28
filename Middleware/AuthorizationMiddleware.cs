using HelpFinance.Data;
using HelpFinance.Models;
using HelpFinance.Models.DTOs.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HelpFinance.Middleware
{
    public class AuthorizationMiddleware : IAuthorizationMiddleware
    {
        private readonly AppDataContext _ctx;
        private readonly JwtOptions _jwt;
        private readonly IConfiguration _config;

        public AuthorizationMiddleware(AppDataContext ctx, IOptions<JwtOptions> jwt, IConfiguration config)
        {
            _ctx = ctx;
            _jwt = jwt.Value;
            _config = config;
        }

        public async Task<TokenResponse?> LoginAsync(string login, string senha)
        {
            var user = await _ctx.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(u => u.LOGIN == login);
            var hasher = new PasswordHasher<UsuarioModel>();
            if (user is null) return null;
            var result = hasher.VerifyHashedPassword(user, user.SENHA, senha);
            if (result != PasswordVerificationResult.Success)
                return null;

            var now = DateTime.UtcNow;

            var access = GenerateToken(user.ID.ToString(), _config.GetValue<int>("Jwt:ExpiresMinutes")); // 15 min
            var refresh = GenerateToken(user.ID.ToString(), 60 * 24 * 7); // 7 dias

            return new TokenResponse
            {
                AccessToken = access.Token,
                ExpiresAt = access.Expires,
                RefreshToken = refresh.Token,
                RefreshExpiresAt = refresh.Expires
            };
        }


        public (string Token, DateTime Expires) GenerateToken(string userId, int expiresMinutes)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(expiresMinutes);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }



    }
}
