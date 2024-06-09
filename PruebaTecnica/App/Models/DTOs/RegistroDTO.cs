using PruebaTecnica.App.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.App.Models.DTOs
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La primer apellido es requerido")]
        public string Apellido1 { get; set; }

        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es requerida")]
        public string IdTipoIdentificacion { get; set; }

        [Required(ErrorMessage = "La identificación es requerida")]
        [IdentificacionValidator]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Se debe proporcionar un email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$", ErrorMessage = "La contraseña debe contener mínimo 6 caracteres que debe contener números y letras como requisito")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol del usuario es requerido")]
        public string IdRolUsuario { get; set; }

        public List<string> Telefonos { get; set; }

        public List<int?> HabilidadesBlandas { get; set; }
    }
}
