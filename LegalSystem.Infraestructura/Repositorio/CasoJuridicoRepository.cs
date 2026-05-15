using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Domain;
using LegalSystem.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace LegalSystem.Infraestructura.Repositorio
{
    public class CasoJuridicoRepository : ICasoJuridicoRepository
    {
        private readonly ApplicationDbContext _context;

        public CasoJuridicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CasoJuridico>> GetAllAsync()
        {
            // Esto trae todos los casos de la tabla CasosJuridicos incluyendo al Cliente
            return await _context.CasosJuridicos
                .Include(c => c.Cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<CasoJuridico>> GetAllPagedAsync(int pagina, int tamano)
        {
            return await _context.CasosJuridicos
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<IEnumerable<CasoJuridico>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            return await _context.CasosJuridicos
                .Where(c => c.TituloCaso.Contains(valor)) 
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<int> CountAsync() => await _context.CasosJuridicos.CountAsync();

        public async Task<int> CountSearchAsync(string valor) =>
            await _context.CasosJuridicos.Where(c => c.TituloCaso.Contains(valor)).CountAsync();

        public async Task<CasoJuridico?> GetByIdAsync(int id) => await _context.CasosJuridicos.FindAsync(id);

        public async Task<CasoJuridico?> GetByIdWithDetailsAsync(int id)
        {
            // Aquí usamos Include para traer el cliente relacionado al caso
            return await _context.CasosJuridicos
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Casoid == id);
        }

        public async Task AddAsync(CasoJuridico caso)
        {
            await _context.CasosJuridicos.AddAsync(caso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CasoJuridico caso)
        {
            _context.CasosJuridicos.Update(caso);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var caso = await _context.CasosJuridicos.FindAsync(id);
            if (caso != null)
            {
                _context.CasosJuridicos.Remove(caso);
                await _context.SaveChangesAsync();
            }
        }
    }
}