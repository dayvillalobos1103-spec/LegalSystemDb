
namespace LegalSystem.Application.DTOs.CasoJuridico
{
    public class CrearCasoDtos
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public int ClienteId { get; set; }
    }
}
