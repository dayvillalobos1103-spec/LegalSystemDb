using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class ActualizarCitaRequest
    {
        [Required(ErrorMessage = "El ID de la cita es obligatorio.")]
        public int Citaid { get; set; }

        [Required(ErrorMessage = "Debe programar una fecha.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El lugar de la cita no puede quedar vacío.")]
        public string Lugar { get; set; } = null!;

        [Required(ErrorMessage = "Debe indicar a qué cliente pertenece la cita.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La cita debe estar ligada a un caso.")]
        public int Casoid { get; set; }
    }
}