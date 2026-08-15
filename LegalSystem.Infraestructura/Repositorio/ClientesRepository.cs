using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Domain;
using LegalSystem.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace LegalSystem.Infraestructura.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Paginación: Skip salta las páginas anteriores, Take trae solo la cantidad necesaria
        public async Task<IEnumerable<Clientes>> GetAllPagedAsync(int pagina, int tamano, string? usuarioId = null)
        {
            var query = _context.Clientes
                .Include(c => c.Usuario)
                .Include(c => c.CasosJuridicos) 
                .Include(c => c.Citas)          
                .AsNoTracking();

            if (!string.IsNullOrEmpty(usuarioId))
            {
                query = query.Where(c => c.UsuarioId == usuarioId);
            }

            return await query
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? usuarioId = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(usuarioId))
            {
                query = query.Where(c => c.UsuarioId == usuarioId);
            }

            return await query.CountAsync();
        }
        // Búsqueda paginada (ej. buscar por nombre)
        public async Task<IEnumerable<Clientes>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            return await _context.Clientes
                .Where(c => c.Nombre.Contains(valor))
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<int> CountAsync() => await _context.Clientes.CountAsync();

        public async Task<int> CountSearchAsync(string valor) =>
            await _context.Clientes.Where(c => c.Nombre.Contains(valor)).CountAsync();

        public async Task<Clientes?> GetByIdAsync(int id)
        {
            // Busca al cliente por su llave primaria
            return await _context.Clientes.FindAsync(id);
        }

        public async Task AddAsync(Clientes cliente)
        {
            // Agrega al contexto y guarda en la BD
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Clientes cliente)
        {
            // Marca el cliente como modificado y guarda
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Buscamos el objeto primero
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Clientes>> GetAllAsync(string? usuarioId = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(usuarioId))
            {
                query = query.Where(c => c.UsuarioId == usuarioId);
            }

            return await query.ToListAsync();
        }

    }
}