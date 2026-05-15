
using LegalSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace LegalSystem.Application.Interfaces.Dbcontex
{
    public interface IApplicationDbContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Clientes> Clientes { get; set; }
        DbSet<CasoJuridico> CasosJuridicos { get; set; }
        DbSet<Cita> Citas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
