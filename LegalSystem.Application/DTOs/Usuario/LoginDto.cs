using System.ComponentModel.DataAnnotations;

namespace LegalSystem.Application.DTOs.Usuario
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Debe ingresar su correo electrónico.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña no puede estar vacía.")]
        public string Password { get; set; } = null!;
    }
}