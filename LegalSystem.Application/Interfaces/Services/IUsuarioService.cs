using LegalSystem.Application.DTOs.Usuario;
using System.Threading.Tasks;

namespace LegalSystem.Application.Interfaces
{
    public interface IUsuarioService
    {
        // Para registrar nuevos usuarios (Abogados, Admin, etc.)
        Task<bool> RegistrarUsuarioAsync(CrearUsuarioDtos dto);

        // Para validar el acceso al sistema
        Task<string?> LoginAsync(string email, string password);

        Task<bool> EliminarUsuarioAsync(string email);
        Task<bool> ActualizarUsuarioAsync(string email, string nuevoNombre);
        Task<int> ObtenerTotalUsuariosRegistradosAsync();
      
    }
}
