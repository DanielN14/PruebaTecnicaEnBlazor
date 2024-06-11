using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Models.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace PruebaTecnica.App.Services.Security
{
        public class CustomAuthenticationProvider : AuthenticationStateProvider
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CustomAuthenticationProvider(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public override Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var user = httpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(user));
            }

            public void NotifyAuthenticationStateChanged()
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var user = httpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }

        //private readonly ProtectedSessionStorage _sessionStorage;
        //private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());



        /*public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                // Si el usuario ya está autenticado, devolver el estado actual
                return Task.FromResult(new AuthenticationState(httpContext.User));
            }

            // Comprobar si hay una cookie de autenticación
            var authCookie = httpContext.Request.Cookies["authCookie"];
            if (string.IsNullOrEmpty(authCookie))
            {
                // Si no hay cookie, devolver un usuario no autenticado
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            // Deserializar la cookie para obtener los claims
            var claims = JsonConvert.DeserializeObject<List<Claim>>(authCookie);
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task MarkUserAsAuthenticatedAsync(SesionUsuario sesionUsuario)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, sesionUsuario.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario),
                new Claim(ClaimTypes.Role, sesionUsuario.RolUsuario)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(identity);

            // Serializar los claims y guardarlos en una cookie
            var serializedClaims = JsonConvert.SerializeObject(claims);
            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddSeconds(30) };
    
            httpContext.Response.Cookies.Append("authCookie", serializedClaims, cookieOptions);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
*/

        /*public async Task MarkUserAsLoggedOutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext.Response.Cookies.Delete("authCookie");

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }*/







        /*public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<SesionUsuario>("sesionUsuario");
                var sesionUsuario = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

                if (sesionUsuario == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                       new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Name, sesionUsuario.NombreUsuario),
                        new Claim(ClaimTypes.Role, sesionUsuario.RolUsuario)
                    }, "AutorizacionCustom"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }*/

        /*public async Task UpdateAuthenticationState(SesionUsuario sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                await _sessionStorage.SetAsync("sesionUsuario", sesionUsuario);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, sesionUsuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, sesionUsuario.RolUsuario)
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("sesionUsuario");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }*/
    }
}
