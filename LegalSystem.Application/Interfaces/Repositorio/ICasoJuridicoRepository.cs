using LegalSystem.Domain;


namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface ICasoJuridicoRepository
    {
        Task<IEnumerable<CasoJuridico>> GetAllPagedAsync(int pagina, int tamano, string? usuarioId = null);
        Task<int> CountAsync(string? usuarioId = null);

        // Búsqueda paginada
        Task<IEnumerable<CasoJuridico>> SearchPagedAsync(string valor, int pagina, int tamano);

        // Contadores
        Task<int> CountAsync();
        Task<int> CountSearchAsync(string valor);

        // CRUD Básico
        Task<CasoJuridico?> GetByIdAsync(int id);
        Task AddAsync(CasoJuridico caso);
        Task UpdateAsync(CasoJuridico caso);
        Task DeleteAsync(int id);

        // Método especializado 
        Task<CasoJuridico?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<CasoJuridico>> GetAllAsync(string? usuarioId = null);
    }
}