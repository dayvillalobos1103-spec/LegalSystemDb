using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class ActualizarClienteRequest
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int Clienteid { get; set; }

        [Required(ErrorMessage = "El nombre no puede quedar vacío.")]
        public string Nombre { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Email { get; set; } = null!;

        public string? Telefono { get; set; }
        public string Domicilio { get; set; } = null!;
    }
}