using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalSystem.Domain;

namespace LegalSystem.Application.DTOs.Cita
{
    public class CitaDtos
    {

        public int Citaid { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = null!;
        public string Lugar { get; set; } = null!;

        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; } = null!;
        public int Casoid { get; set; }
        public string TituloCaso { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        

    }
}
