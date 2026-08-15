namespace LegalSystem.Application.DTOs.CasoJuridico
{
    public class ActualizarCasoDtos
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int ClienteId { get; set; }
    }
}
