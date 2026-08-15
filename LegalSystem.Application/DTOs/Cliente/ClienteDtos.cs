using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Cliente
{
    public class ClienteDtos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;

        public string UsuarioId { get; set; } = string.Empty;



        // FUNCIONAMINETO EXTRA DEL MOVIL
        public string TituloCaso { get; set; } = string.Empty;
        public string DescripcionCaso { get; set; } = string.Empty;
        public string DetalleCita { get; set; } = string.Empty;

    }
}
