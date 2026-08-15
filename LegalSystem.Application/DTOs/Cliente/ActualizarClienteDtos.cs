namespace LegalSystem.Application.DTOs.Cliente
{
    public class ActualizarClienteDtos
    {
        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Telefono { get; set; }

        public string Domicilio { get; set; } = null!;

        
       
    }
}
