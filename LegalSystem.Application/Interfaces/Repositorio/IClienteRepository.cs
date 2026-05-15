using LegalSystem.Domain;

namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface IClienteRepository
    {
        // Paginación y Listado
        Task<IEnumerable<Clientes>> GetAllPagedAsync(int pagina, int tamano);

        // Búsqueda con paginación
        Task<IEnumerable<Clientes>> SearchPagedAsync(string valor, int pagina, int tamano);

        // Contadores para el paginador del Frontend
        Task<int> CountAsync();
        Task<int> CountSearchAsync(string valor);

        // CRUD Básico (Mantener estos es esencial)
        Task<Clientes?> GetByIdAsync(int id);
        Task AddAsync(Clientes cliente);
        Task UpdateAsync(Clientes cliente);
        Task DeleteAsync(int id);
        Task<IEnumerable<Clientes>> GetAllAsync();
    }
}
