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
    }
}