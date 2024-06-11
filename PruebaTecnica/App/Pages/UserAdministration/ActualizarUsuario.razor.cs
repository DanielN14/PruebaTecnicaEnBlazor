using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PruebaTecnica.App.Models.DTOs;
using PruebaTecnica.App.Services;
using PruebaTecnica.App.Services.Security;
using PerSecurity = PruebaTecnica.App.Services.Security;

namespace PruebaTecnica.App.Pages.UserAdministration
{
    public partial class ActualizarUsuario
    {
        [Inject] public UserAdministrationService _UserAdministration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        //[Inject] private AuthenticationStateProvider _AuthenticationProvider { get; set; }


        private Task<AuthenticationState>? authenticationStateTask { get; set; }
        private RegistroDTO _registroDTO { get; set; } = new RegistroDTO();

        [Parameter] public int idUsuario { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //authenticationStateTask = _AuthenticationProvider.GetAuthenticationStateAsync();

            var authState = await authenticationStateTask;
            var user = authState.User;

            if (idUsuario == 0)
            {
                NavigationManager.NavigateTo("/UserAdministration");
            }

            _registroDTO = ObtenerDatosUsuario(idUsuario);
            Console.WriteLine(_registroDTO);
        }


        private RegistroDTO ObtenerDatosUsuario(int idUsuario)
        {
            return _UserAdministration.CargarUsuario(idUsuario);
        }

        public void ActualizarUsuarioEvent()
        {
            try
            {
                object respuesta = _UserAdministration.ActualizarUsuario(idUsuario, _registroDTO);

                if (respuesta.ToString() != "1")
                {
                    Console.WriteLine("Salida");
                    return;
                }

                NavigationManager.NavigateTo("/UserAdministration");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}