using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Cita
{
    public class CrearCitaDtos
    {
        public DateTime FechaHora { get; set; } 
        public string Motivo { get; set; } = null!;
        public string Lugar { get; set; } = null!;
        public string Estado { get; set; } = "programada";
        public int ClienteId { get; set; }
        public int? Casoid { get; set; } 
    }
}
