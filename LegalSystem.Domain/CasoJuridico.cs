

using System.ComponentModel.DataAnnotations.Schema;

namespace LegalSystem.Domain
{
    public class CasoJuridico
    {
        public int Casoid { get; set; }
        public string TituloCaso { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public int ClienteId { get; set; } //

        // Cambiamos idusuario por UsuarioId
        public string UsuarioId { get; set; } = null!; //

        public virtual Clientes Cliente { get; set; } = null!; //
        public virtual Usuario Usuario { get; set; } = null!; //
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();


    }
}
