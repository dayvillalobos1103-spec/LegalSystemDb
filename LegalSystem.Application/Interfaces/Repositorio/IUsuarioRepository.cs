using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalSystem.Domain;

namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface IUsuarioRepository
    {
        // CRUD Básico
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        // Buscamos un usuario por su email para verificar su contraseña
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
