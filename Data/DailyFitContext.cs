using Microsoft.EntityFrameworkCore;
using DailyFit.Models;
namespace DailyFit.Data;

public class DailyFitContext : DbContext
{
    public DailyFitContext(DbContextOptions<DailyFitContext> opts) : base(opts)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){

    }
    public DbSet<Usuario> Usuarios { get; set; }
}