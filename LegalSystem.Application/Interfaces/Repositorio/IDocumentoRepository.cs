using LegalSystem.Domain.Entities; // Ajusta según el namespace de tu entidad Documentos
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace LegalSystem.Application.Interfaces.Repositorio
{
    public interface IDocumentoRepository
    {
        Task<Documento> AddAsync(Documento documento);
        Task<IEnumerable<Documento>> GetAllByUsuarioIdAsync(string usuarioId);
        Task<Documento> GetByIdAsync(int id);
        Task<bool> DeleteAsync(Documento documento);
    }
}
