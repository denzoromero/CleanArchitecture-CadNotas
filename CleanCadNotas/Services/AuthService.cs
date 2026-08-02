using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesBS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CleanCadNotas.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(UsuarioBS user)
        {
            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(ClaimTypes.Name,   user.Chapa),
                                new Claim("Chapa",   user.Chapa!),
                                new Claim("SessionId", Guid.NewGuid().ToString()),
                            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

    }
}
