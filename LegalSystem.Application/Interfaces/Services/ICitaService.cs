
using LegalSystem.Application.DTOs.Cita;
using LegalSystem.Application.Response;
using System.Threading.Tasks;

namespace LegalSystem.Application.Interfaces
{
    public interface ICitaService
    {
        // 1. Paginación y Listado
        Task<RespuestaPaginada<CitaDtos>> GetAllPagedAsync(int pagina, int tamano);

        // 2. Búsqueda con paginación ( buscar citas por motivo o cliente)
        Task<RespuestaPaginada<CitaDtos>> SearchPagedAsync(string valor, int pagina, int tamano);

        // 3. CRUD 
        Task<CitaDtos?> GetByIdAsync(int id); 
        Task<bool> AddAsync(CrearCitaDtos dto);
        Task<bool> UpdateAsync(int id, ActualizarCitaDtos dto); 
        Task<bool> DeleteAsync(int id);
}   }   