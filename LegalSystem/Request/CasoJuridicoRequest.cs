using System.ComponentModel.DataAnnotations;

namespace LegalSystem.API.Request
{
    public class CasoJuridicoRequest
    {
        [Required(ErrorMessage = "El título del caso es indispensable.")]
        [MaxLength(150, ErrorMessage = "El título es demasiado largo.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "Debe describir brevemente el asunto legal.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El id del cliente es obligatorio para vincular el caso.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un cliente válido.")]
        public int ClienteId { get; set; }

        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public int Pagina { get; set; } = 1;
        public int Tamano { get; set; } = 10;

    }
}
