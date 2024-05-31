using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Models.DTOs;
using PruebaTecnica.App.Services;

namespace PruebaTecnica.App.Pages.UserAdministration
{
    public partial class ActualizarUsuario
    {

        [Inject] public UserAdministrationService _UserAdministration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private RegistroDTO _registroDTO { get; set; } = new RegistroDTO();

        [Parameter] public int idUsuario { get; set; }

        protected override void OnInitialized()
        {
            if(idUsuario == null)
            {
                NavigationManager.NavigateTo("/UserAdministration");
            }

            _registroDTO = ObtenerDatosUsuario(idUsuario);
        }

        private RegistroDTO ObtenerDatosUsuario(int idUsuario)
        {
            return _UserAdministration.CargarUsuario(idUsuario);
        }

        public void ActualizarUsuarioEvent()
        {
            try
            {
                //object respuesta = _UserAdministration.InsertarUsuario(_registroDTO);

               /* if (respuesta.ToString() != "1")
                {
                    Console.WriteLine("Salida");
                    return;
                }*/

                NavigationManager.NavigateTo("/UserAdministration");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}