using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class CitaRequest
    {
        [Required(ErrorMessage = "La fecha y hora de la cita son obligatorias.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "Debe indicar el motivo de la reunión.")]
        [StringLength(200, ErrorMessage = "El motivo no puede exceder los 200 caracteres.")]
        public string Motivo { get; set; } = null!;

        [Required(ErrorMessage = "La cita debe estar asociada a un caso jurídico.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del caso no es válido.")]
        public int CasoJuridicoId { get; set; }

        public string? Lugar { get; set; }
        public int Pagina { get; set; } = 1;
        public int Tamano { get; set; } = 10;
    }
}
