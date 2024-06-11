using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PruebaTecnica.App.Models;
using System.Security.Claims;
using PruebaTecnica.App.Services.Security.Interfaces;

namespace PruebaTecnica.App.Services.Security
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LoginUserAsync(SesionUsuario sesionUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, sesionUsuario.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario),
                new Claim(ClaimTypes.Role, sesionUsuario.RolUsuario)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(20) // Ajusta el tiempo de expiración según sea necesario
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }

        public async Task LogoutUserAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
