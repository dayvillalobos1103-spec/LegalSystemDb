using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.DTOs.Casoluridico;
using LegalSystem.Application.DTOs.Casoluridico.LegalSystem.Application.DTOs.Casojuridico;
using LegalSystem.Application.Response;
using System.Threading.Tasks;

namespace LegalSystem.Application.Interfaces
{
    public interface ICasoService
    {
        // 1. Paginación y Listado
        Task<RespuestaPaginada<CasoDtos>> GetAllPagedAsync(int pagina, int tamano);

        // 2. Búsqueda con paginación (ej. buscar por título o expediente)
        Task<RespuestaPaginada<CasoDtos>> SearchPagedAsync(string valor, int pagina, int tamano);

        // 3. CRUD Básico
        Task<CasoDtos?> GetByIdAsync(int id);
        Task<bool> AddAsync(CrearCasoDtos dto);
        Task<bool> UpdateAsync(ActualizarCasoDtos dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CasoDtos>> GetAllAsync();
    }
}