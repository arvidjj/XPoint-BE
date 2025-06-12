using Microsoft.EntityFrameworkCore;
using XPointBE.Models;

namespace XPointBE.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
    }

}