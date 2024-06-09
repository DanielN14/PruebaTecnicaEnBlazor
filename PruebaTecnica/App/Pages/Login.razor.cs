using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Models.DTOs;
using System.Data.SqlClient;
using System.Data;
using PerSecurity = PruebaTecnica.App.Services.Security;
using Microsoft.AspNetCore.Components.Authorization;
using PruebaTecnica.App.Services.Security;
using PruebaTecnica.App.Models;

namespace PruebaTecnica.App.Pages
{
    public partial class Login
    {
        [Inject]  private Conexion _conexion { get; set; }
        [Inject] private AuthenticationStateProvider _AuthenticationProvider { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
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

                string idUsuario = res.Rows[0]["IdUsuario"].ToString();

                var customAuthProvider = (CustomAuthenticationProvider)_AuthenticationProvider;
                await customAuthProvider.UpdateAuthenticationState(new SesionUsuario
                {
                    IdUsuario = res.Rows[0]["IdUsuario"].ToString(),
                    NombreUsuario = res.Rows[0]["NombreUsuario"].ToString(),
                    RolUsuario = res.Rows[0]["NombreRol"].ToString()
                });

                if(res.Rows[0]["NombreRol"].ToString() == "Administrador") NavigationManager.NavigateTo("/UserAdministration");
                if (res.Rows[0]["NombreRol"].ToString() == "Consultor") NavigationManager.NavigateTo("/home");
            }
            catch(Exception ex) {
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