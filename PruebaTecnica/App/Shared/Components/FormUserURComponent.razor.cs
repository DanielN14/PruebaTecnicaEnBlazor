using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Models.DTOs;
using PruebaTecnica.App.Services;
using System.Text.RegularExpressions;

namespace PruebaTecnica.App.Shared.Components
{
    public partial class FormUserURComponent
    {
        [Inject] public UserAdministrationService _UserAdministration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        
        [Parameter] public RegistroDTO _registroDTO { get; set; }
        [Parameter] public EventCallback<RegistroDTO> OnSubmit { get; set; }


        private List<string> telefonos = new List<string>();
        public List<int?> SelectedHabilidadesBlandas = new List<int?>();

        public string textoBtnFormulario { get; set; } = "Registrar Usuario";
        public string nuevoTelefono { get; set; }
        private bool mostrarErrorTelefono = false;
        private string mensajeErrorTelefono = "";
        private bool mostrarErrorHB = false;
        private string mensajeErrorHB = "";


        protected override void OnInitialized()
        {
            LlenarTelefonosUsuario();
            MarcarHabilidadesBlandasUsuario();

            if (_registroDTO != null) textoBtnFormulario = "Actualizar Usuario";
        }

        private async Task OnSubmitEvent()
        {
            if (!Validar3HabilidadesBlandas())
            {
                mostrarErrorHB = true;
                mensajeErrorHB = "Debe seleccionar al menos 3 habilidades blandas.";
                return;
            }

            mostrarErrorHB = false;

            _registroDTO.Telefonos = telefonos;
            _registroDTO.HabilidadesBlandas = SelectedHabilidadesBlandas;

            await OnSubmit.InvokeAsync(_registroDTO);
        }

        private void AgregarTelefono()
        {
            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
            {
                if (!Regex.IsMatch(nuevoTelefono, @"^\d{8}$"))
                {
                    mostrarErrorTelefono = true;
                    mensajeErrorTelefono = "El número de teléfono debe tener 8 dígitos numéricos.";
                    return;
                }

                telefonos.Add(nuevoTelefono);
                nuevoTelefono = string.Empty;
                mostrarErrorTelefono = false;
            }
            else
            {
                mostrarErrorTelefono = false;
            }

            StateHasChanged();
        }

        private void EliminarTelefono(string telefono)
        {
            telefonos.Remove(telefono);
        }

        private void CheckboxChangedHB(ChangeEventArgs e, int habilidadBlandaId)
        {
            if ((bool)e.Value)
            {
                if (!SelectedHabilidadesBlandas.Contains(habilidadBlandaId))
                {
                    SelectedHabilidadesBlandas.Add(habilidadBlandaId);
                }
            }
            else
            {
                if (SelectedHabilidadesBlandas.Contains(habilidadBlandaId))
                {
                    SelectedHabilidadesBlandas.Remove(habilidadBlandaId);
                }
            }
        }

        private bool Validar3HabilidadesBlandas()
        {
            if (SelectedHabilidadesBlandas.Count >= 3)
            {
                return true;
            }

            return false;
        }

        public List<TipoIdentificacion> CargarTipoIdentificacion()
        {
            try
            {
                return _UserAdministration.CargarTipoIdentificacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RolUsuario> CargarRolesUsuario()
        {
            try
            {
                return _UserAdministration.CargarRolesUsuario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HabilidadBlanda> CargarHabilidadesBlandas()
        {
            try
            {
                return _UserAdministration.CargarHabilidadesBlandas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public void MarcarHabilidadesBlandasUsuario()
        {
            if(_registroDTO.HabilidadesBlandas != null && _registroDTO.HabilidadesBlandas.Count > 0)
            {
                foreach (var habilidadBlanda in _registroDTO.HabilidadesBlandas)
                {
                    SelectedHabilidadesBlandas.Add(habilidadBlanda);
                }
            }
        }

        public void LlenarTelefonosUsuario()
        {
            if (_registroDTO.Telefonos != null && _registroDTO.Telefonos.Count > 0)
            {
                foreach (var telefono in _registroDTO.Telefonos)
                {
                    telefonos.Add(telefono);
                }
            }
        }
    }
}