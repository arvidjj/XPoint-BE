using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XPointBE.Models;
using XPointBE.Models.Usuarios;

namespace XPointBE.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
            new IdentityRole { Id = "3", Name = "Empleado", NormalizedName = "EMPLEADO" }
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }

}