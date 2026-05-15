using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Cita
{
    public class ActualizarCitaDtos
    {
        // El ID lo recibes por la URL (no en el DTO)
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = null!;
        public string Lugar { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
