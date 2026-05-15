using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Cliente
{
    public class CrearClienteDtos
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Domicilio { get; set; } = null!;

    
    }
}
