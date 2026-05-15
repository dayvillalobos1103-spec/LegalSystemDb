namespace LegalSystem.Application.DTOs.Casoluridico
{
    namespace LegalSystem.Application.DTOs.Casojuridico
    {
        public class ActualizarCasoDtos
        {
            public int Casoid { get; set; }
            public string Titulo { get; set; } = null!;
            public string Descripcion { get; set; } = null!;
            public int ClienteId { get; set; }
            
            public string UsuarioId { get; set; } = null!;
        }
    }
}
