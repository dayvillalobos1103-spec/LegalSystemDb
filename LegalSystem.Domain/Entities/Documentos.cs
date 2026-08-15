using LegalSystem.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalSystem.Domain.Entities // Ajusta el namespace según tu proyecto
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string RutaUrl { get; set; } = string.Empty;

        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;


        // --- Relaciones ---
        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Clientes Cliente { get; set; }

        public int? CasoId { get; set; } // Es opcional, puede que el documento sea solo del cliente y no de un caso

        [Required]
        public string UsuarioId { get; set; } = string.Empty; // Para aislar los documentos por abogado
    }
}