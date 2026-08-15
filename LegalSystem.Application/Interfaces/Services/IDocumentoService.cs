using System.Collections.Generic;
using System.Threading.Tasks;
using LegalSystem.Application.DTOs;

namespace LegalSystem.Application.Interfaces.Services
{
    public interface IDocumentoService
    {
        Task<DocumentoDtos> CrearDocumentoAsync(CrearDocumentoDto dto, string usuarioId);
        Task<IEnumerable<DocumentoDtos>> GetAllByUsuarioIdAsync(string usuarioId);
        Task<bool> DeleteAsync(int id, string usuarioId);
    }
}