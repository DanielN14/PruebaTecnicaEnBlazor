using Microsoft.AspNetCore.Components.Authorization;
using PruebaTecnica.App.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PruebaTecnica.App.Services.Security
{
    public class AuthenticationService //: AuthenticationStateProvider
    {
        /*private readonly AuthenticationService _authenticationService;

        public AuthenticationService(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _authenticationService.GetUserAsync(); // Implementa este método para obtener el usuario actualmente autenticado
            var identity = user != null ? new ClaimsIdentity(GetUserClaims(user), "custom authentication type") : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private IEnumerable<Claim> GetUserClaims(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                //new Claim(ClaimTypes.Name, usuario.),
                // Agregar más reclamaciones según sea necesario
            };

            // Agregar reclamaciones de roles
            foreach (var role in user.Roles)
            {
            }
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        public async Task SetAuthenticationStateAsync(AuthenticationState authenticationState)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }*/
    }
}
