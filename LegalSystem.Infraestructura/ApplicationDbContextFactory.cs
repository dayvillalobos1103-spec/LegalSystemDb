using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LegalSystem.Infraestructura.Data; // Asegúrate que este sea tu namespace

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // AQUÍ PONES TUS VALORES FIJOS PARA LA MIGRACIÓN
        //var connectionString = "Host=127.0.0.1;Port=5432;Database=LegalSystem;Username=postgres;Password=perla123";
         var connectionString = "Host=dpg-d9valpvlk1mc738gi02g-a.ohio-postgres.render.com;Port=5432;Database=legalsystem_db;Username=legalsystem_db_user;Password=NznNj5ElirPg8pA5SLWCjNt8ki7FQRtN;Ssl Mode=Require;Trust Server Certificate=true";
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}