using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystem.Application.DTOs.Usuario
{
    public class CrearUsuarioDtos
    {
        
            public string Nombre { get; set; } = null!;
            public string Email { get; set; } = null!;
                //solo al inicio de sesion 
            public string Password { get; set; } = null!;
            public string ConfirmarPassword { get; set; } = null!;
        
    }
}
