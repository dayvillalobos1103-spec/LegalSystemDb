using LegalSystem.Domain;

namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface ICitaRepository
    {
        // Paginación y Listado
        Task<IEnumerable<Cita>> GetAllPagedAsync(int pagina, int tamano, string? usuarioId = null);
        Task<int> CountAsync(string? usuarioId = null);

        // Contadores y Búsqueda
        Task<int> CountSearchAsync(string valor, string? usuarioId = null);
        Task<IEnumerable<Cita>> SearchPagedAsync(string valor, int pagina, int tamano, string? usuarioId = null);

        // CRUD Básico
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        Task UpdateAsync(Cita cita);
        Task DeleteAsync(int id);

        // Método especializado para ver detalles (Cita + Cliente + Caso)
        Task<Cita?> GetByIdWithDetailsAsync(int id);
        
    }
}