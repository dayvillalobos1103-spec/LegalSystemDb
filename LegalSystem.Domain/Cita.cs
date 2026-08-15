

using System.ComponentModel.DataAnnotations.Schema;

namespace LegalSystem.Domain
{
    public class Cita
    {
        public int Citaid { get; set; } // Llave primaria
        public string Motivo { get; set; } = null!; // Requerido, Máx 200
        public string? Lugar { get; set; } // Máx 150
        public DateTime FechaHora { get; set; } // Requerido
        public string Estado { get; set; } = "programada"; // Máx 200, Valor por defecto

        // Llaves foráneas
        public int ClienteId { get; set; }
        public virtual Clientes Cliente { get; set; } = null!;

        public int? Casoid { get; set; }
        public virtual CasoJuridico? CasosJuridico { get; set; }

        // El cambio clave: UsuarioId en lugar de idusuario
        public string UsuarioId { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;

    }
}
