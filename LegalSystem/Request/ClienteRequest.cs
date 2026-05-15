using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class ClienteRequest
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La identificación o cédula es requerida.")]
        [RegularExpression(@"^[0-9A-Z-]+$", ErrorMessage = "La identificación tiene un formato inválido.")]
        public string Cedula { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Telefono { get; set; } = null!;
    }
}
