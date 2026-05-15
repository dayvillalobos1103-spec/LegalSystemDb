using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Usuario
{
    public class UsuarioDtos
    {

        public string UsuarioId { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
       
        public string Rol { get; set; } = null!;

    }
}
