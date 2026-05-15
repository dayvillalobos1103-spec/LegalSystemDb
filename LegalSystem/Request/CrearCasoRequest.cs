using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class CrearCasoRequest
    {
        [Required(ErrorMessage = "El título del caso es obligatorio.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El título debe tener entre 10 y 100 caracteres.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es necesaria para entender el caso.")]
        [MinLength(20, ErrorMessage = "La descripción debe ser más detallada (mínimo 20 caracteres).")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "Debe asignar este caso a un cliente.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido.")]
        public int ClienteId { get; set; }
    }
}