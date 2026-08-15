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
        public string? Motivo { get; set; }
        public string? Lugar { get; set; }
        public string? Estado { get; set; }



        // Datos del Cliente (Planos, sin objetos anidados)
        public int ClienteId { get; set; }
        public string? NombreCliente { get; set; }

        // Datos del Abogado / Usuario
      
        public string? NombreAbogado { get; set; }

        // Datos del Caso Jurídico
        public int? Casoid { get; set; }
        public string? TituloCaso { get; set; }


    }
}
