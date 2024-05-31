using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.App.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El email es requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerido")]
        public string Password { get; set; }
    }
}
