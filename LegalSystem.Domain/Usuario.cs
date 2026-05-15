using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LegalSystem.Domain
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; } = null!;
        public ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();
    }
}