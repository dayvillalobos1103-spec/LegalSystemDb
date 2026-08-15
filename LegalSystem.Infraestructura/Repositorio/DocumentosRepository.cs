using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Domain.Entities;
using LegalSystem.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalSystem.Infraestructura.Repositorio
{
    public class DocumentosRepository : IDocumentoRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Documento> AddAsync(Documento documento)
        {
            await _context.Documentos.AddAsync(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task<IEnumerable<Documento>> GetAllByUsuarioIdAsync(string usuarioId)
        {
            return await _context.Documentos
                                 .Where(d => d.UsuarioId == usuarioId)
                                 .OrderByDescending(d => d.FechaSubida) // Los más recientes primero
                                 .ToListAsync();
        }

        public async Task<Documento> GetByIdAsync(int id)
        {
            return await _context.Documentos.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Documento documento)
        {
            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
