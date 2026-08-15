using System;

namespace LegalSystem.Application.DTOs // Ajusta tu namespace
{
    public class DocumentoDtos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RutaUrl { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
        public int ClienteId { get; set; }
        public int? CasoId { get; set; }
    }
}