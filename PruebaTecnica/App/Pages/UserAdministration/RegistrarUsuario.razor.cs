using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Models.DTOs;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using PruebaTecnica.App.Services;
using System.Text.RegularExpressions;

namespace PruebaTecnica.App.Pages.UserAdministration
{
    public partial class RegistrarUsuario
    {
        [Inject] public UserAdministrationService _UserAdministration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private RegistroDTO _registroDTO { get; set; } = new RegistroDTO();


        /*private List<string> telefonos = new List<string>();
        public List<int> SelectedHabilidadesBlandas = new List<int>();

        public string nuevoTelefono { get; set; }
        private bool mostrarErrorTelefono = false;
        private string mensajeErrorTelefono = "";
        private bool mostrarErrorHB = false;
        private string mensajeErrorHB = "";*/


        public void RegistrarUsuarioEvent()
        {
            try
            {
                object respuesta = _UserAdministration.InsertarUsuario(_registroDTO);

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

        /*private void AgregarTelefono()
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
        }*/
    }
}