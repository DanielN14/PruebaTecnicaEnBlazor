using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Models.DTOs;
using System.Data.SqlClient;
using System.Data;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Services.Security;

namespace PruebaTecnica.App.Pages
{
    public partial class LoginPage
    {
        [Inject]  private Conexion _conexion { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private AuthService _authService { get; set; }

        [SupplyParameterFromForm]
        private LoginDTO _loginDTO { get; set; } = new LoginDTO();
        
        public async Task LoginUser()
        {
            try
            {
                DataTable res = _conexion.ExecSPWithOutput("ValidarUsuario", PametroValidarUsuario(_loginDTO)).Tables[0];

                if (res.Rows[0]["EsCredencialCorrecta"].ToString() != "1")
                {
                    return;
                }

                SesionUsuario usuario = new SesionUsuario()
                {
                    IdUsuario = res.Rows[0]["IdUsuario"].ToString(),
                    NombreUsuario = res.Rows[0]["NombreUsuario"].ToString(),
                    RolUsuario = res.Rows[0]["NombreRol"].ToString()
                };

                await _authService.LoginUserAsync(usuario);

                if (usuario.RolUsuario == "Administrador")
                {
                    NavigationManager.NavigateTo("/UserAdministration");
                };

                if (usuario.RolUsuario == "Consultor")
                {
                    NavigationManager.NavigateTo("/home");
                };
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public SqlParameter[] PametroValidarUsuario(LoginDTO data)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = data.UserName});
            parametros.Add(new SqlParameter("@Clave", SqlDbType.VarChar, 50) { Value = data.Password });

            return parametros.ToArray();
        }

    }
}