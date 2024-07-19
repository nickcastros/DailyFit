using Microsoft.EntityFrameworkCore;
using DailyFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace DailyFit.Data;

public class UsuarioContext : IdentityDbContext<Usuario>
{
    public DbSet<DadosPessoais> DadosPessoais => Set<DadosPessoais>();

    public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Usuario>()
            .HasOne(Usuario => Usuario.DadosPessoais)
            .WithOne(DadosPessoais => DadosPessoais.Usuario)
            .HasForeignKey<DadosPessoais>(DadosPessoais => DadosPessoais.usuarioId)
            .OnDelete(DeleteBehavior.Restrict); 
    }

}