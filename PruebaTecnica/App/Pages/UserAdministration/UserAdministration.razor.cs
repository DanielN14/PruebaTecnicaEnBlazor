using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Services;
using System.Data;

namespace PruebaTecnica.App.Pages.UserAdministration
{
    public partial class UserAdministration
    {
        [Inject] private UserAdministrationService _UserAdministration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        public List<Usuario> Usuarios { get; set; }

        protected override void OnInitialized()
        {
            Usuarios = CargarUsuarios();
        }

        public List<Usuario> CargarUsuarios()
        {
            try
            {
                return _UserAdministration.CargarUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarUsuario(int idUsuario)
        {
            NavigationManager.NavigateTo($"/ActualizarUsuario/{idUsuario}");
        }

        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                Usuarios = _UserAdministration.EliminarUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}