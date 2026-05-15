using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Application.Response;
using System.Threading.Tasks;

namespace LegalSystem.Application.Interfaces
{
    public interface IClienteService
    {
        // 1. Paginación y Listado (Devuelve DTOs envueltos en la Respuesta Paginada)
        Task<RespuestaPaginada<ClienteDtos>> GetAllPagedAsync(int pagina, int tamano);

        // 2. Búsqueda con paginación
        Task<RespuestaPaginada<ClienteDtos>> SearchPagedAsync(string valor, int pagina, int tamano);

        Task<IEnumerable<ClienteDtos>> GetAllAsync();

        // 3. CRUD Básico adaptado
        Task<ClienteDtos?> GetByIdAsync(int id);
        Task<bool> AddAsync(CrearClienteDtos dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, ActualizarClienteDtos dto);
    }
}