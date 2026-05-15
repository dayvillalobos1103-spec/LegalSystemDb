using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Domain;
using LegalSystem.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace LegalSystem.Infraestructura.Repositorio
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public CitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cita>> GetAllPagedAsync(int pagina, int tamano)

        {
            return await _context.Citas
            .Include(c => c.Cliente)        // <--- Carga el Cliente
            .Include(c => c.CasosJuridico)  // <--- Carga el Caso
            .Include(c => c.Cliente)        
            .Include(c => c.CasosJuridico)  
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .ToListAsync();
            

        }


        public async Task<IEnumerable<Cita>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            return await _context.Citas
                .Where(c => c.Motivo.Contains(valor)) 
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<int> CountAsync() => await _context.Citas.CountAsync();

        public async Task<int> CountSearchAsync(string valor) =>
            await _context.Citas.Where(c => c.Motivo.Contains(valor)).CountAsync();

        public async Task<Cita?> GetByIdAsync(int id) => await _context.Citas.FindAsync(id);

        public async Task<Cita?> GetByIdWithDetailsAsync(int id)
        {
            // Traemos la cita junto al cliente asociado
            return await _context.Citas
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Citaid == id);
        }

        public async Task AddAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cita cita)
        {
            _context.Citas.Update(cita);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
            }
        }
    }
}