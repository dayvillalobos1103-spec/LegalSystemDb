

namespace LegalSystem.Application.DTOs.CasoJuridico
{
    public class CasoDtos
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }

        // Relación: id y Nombre para identificar quién es el caso
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;

    }
}
