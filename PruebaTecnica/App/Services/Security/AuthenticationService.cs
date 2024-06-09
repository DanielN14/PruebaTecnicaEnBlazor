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

namespace PruebaTecnica.App.Services.Security
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
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
        }

        public async Task UpdateAuthenticationState(SesionUsuario sesionUsuario)
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
        }
    }

    /*public class AuthenticationService : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly Conexion _context;

        public AuthenticationService(Conexion context, ProtectedSessionStorage sessionStorage)
        {
            _context = context;
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<int>("idUsuario");
                var idUsuario = userSessionStorageResult.Success ? userSessionStorageResult.Value : 0;

                if (idUsuario == 0)
                {
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }

                var usuario = ObtenerUsuarioLoggeado(Convert.ToInt32(idUsuario));

                if (usuario == null)
                {
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }

                // Crear un ClaimsIdentity para el usuario autenticado
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, usuario.RolUsuario),
                };

                var identity = new ClaimsIdentity(claims, "custom");

                var principal = new ClaimsPrincipal(identity);

                return await Task.FromResult(new AuthenticationState(principal));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public async Task MarkUserAsAuthenticated(string userId)
        {
            var usuario = ObtenerUsuarioLoggeado(Convert.ToInt32(userId));

            await _sessionStorage.SetAsync("idUsuario", usuario.IdUsuario);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.RolUsuario),

            };

            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _sessionStorage.DeleteAsync("idUsuario");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }


        private Usuario? ObtenerUsuarioLoggeado(int idUsuario)
        {
            DataTable res = _context.ExecSPWithOutput("ObtenerDatosUsuarioLoggeado", PametroValidarUsuario(idUsuario)).Tables[0];

            Usuario usuario = null;

            if (res.Rows.Count > 0)
            {
                usuario = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(res.Rows[0]["IdUsuario"].ToString()),
                    NombreUsuario = res.Rows[0]["NombreUsuario"].ToString(),
                    RolUsuario = res.Rows[0]["NombreRol"].ToString(),
                };
            }

            return usuario;
        }

        private SqlParameter[] PametroValidarUsuario(int idUsuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario });

            return parametros.ToArray();
        }
    }*/
}
