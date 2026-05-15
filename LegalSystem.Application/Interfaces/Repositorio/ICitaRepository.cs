using LegalSystem.Domain;

namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface ICitaRepository
    {
        // Paginación y Listado
        Task<IEnumerable<Cita>> GetAllPagedAsync(int pagina, int tamano);

        // Búsqueda paginada 
        Task<IEnumerable<Cita>> SearchPagedAsync(string valor, int pagina, int tamano);

        // Contadores
        Task<int> CountAsync();
        Task<int> CountSearchAsync(string valor);

        // CRUD Básico
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        Task UpdateAsync(Cita cita);
        Task DeleteAsync(int id);

        // Método especializado para ver detalles (Cita + Cliente + Caso)
        Task<Cita?> GetByIdWithDetailsAsync(int id);
    }
}