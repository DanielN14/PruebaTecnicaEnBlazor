using PruebaTecnica.App.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PruebaTecnica.App.Models.Validators
{
    public class IdentificacionValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance as RegistroDTO;
            if (instance == null) return new ValidationResult("El objeto de validación es nulo.");

            string identificacion = value as string;
            if (string.IsNullOrEmpty(identificacion))
            {
                return new ValidationResult("La identificación es requerida");
            }

            switch (instance.IdTipoIdentificacion)
            {
                case "1":
                    if (!Regex.IsMatch(identificacion, @"^\d{9}$"))
                    {
                        return new ValidationResult("La identificación nacional debe contener exactamente 9 caracteres numéricos.");
                    }
                    break;
            }

            return ValidationResult.Success;
        }
    }
}
