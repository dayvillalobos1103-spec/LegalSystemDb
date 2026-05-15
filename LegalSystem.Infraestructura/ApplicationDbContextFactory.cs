using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LegalSystem.Infraestructura.Data; // Asegúrate que este sea tu namespace

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // AQUÍ PONES TUS VALORES FIJOS PARA LA MIGRACIÓN
        //var connectionString = "Host=localhost;Port=5432;Database=LegalSystem;Username=postgres;Password=perla123";
        var connectionString = "Host=dpg-d83ioqmq1p3s738s1v2g-a.frankfurt-postgres.render.com;Port=5432;Database=legalsystemdb;Username=legalsystemdb_user;Password=NmsRSLqvjyuu3gCqWdX36hE1DhmueHro;SSL Mode=Require;Trust Server Certificate=true";
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}