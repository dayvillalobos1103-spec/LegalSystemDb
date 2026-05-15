

using System.ComponentModel.DataAnnotations.Schema;

namespace LegalSystem.Domain
{
    public class Clientes
    {
        public int Clienteid { get; set; }
        public string Nombre { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Domicilio { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Cambiamos idusuario por UsuarioId para que coincida con la DB
        public string UsuarioId { get; set; } = null!;

        // Propiedad de navegación (sin etiquetas, C# la reconoce sola ahora)
        public virtual Usuario Usuario { get; set; } = null!;

        public ICollection<CasoJuridico> CasosJuridicos { get; set; } = new List<CasoJuridico>();
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
