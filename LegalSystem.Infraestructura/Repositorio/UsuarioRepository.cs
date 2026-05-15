using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Domain;

namespace LegalSystem.Infraestructura.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository

    {
        public Task AddAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
