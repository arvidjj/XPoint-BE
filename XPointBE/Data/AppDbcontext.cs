using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole { Id = "id-role-admin", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "id-role-user", Name = "User", NormalizedName = "USER" },
            new IdentityRole { Id = "id-role-empleado", Name = "Empleado", NormalizedName = "EMPLEADO" }
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        // Seed admin user
        const string ADMIN_USER_ID = "admin-user-id";
        const string ADMIN_ROLE_ID = "id-role-admin";

        var adminUser = new User
        {
            Id = ADMIN_USER_ID,
            Nombre = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = "SEEDING-ADMIN-USER-SECURITY-STAMP",
            ConcurrencyStamp = "SEEDING-ADMIN-USER-CONCURRENCY-STAMP",
            PasswordHash = "AQAAAAIAAYagAAAAEDhY8hUYe9ML7KRWEgirJnVe8yyUBpMPPWunGjzagL5+fV3uq07abey++XHSHIUKgQ==" // This is a hash for "password"
        };

        var userRole = new IdentityUserRole<string>
        {
            UserId = ADMIN_USER_ID,
            RoleId = ADMIN_ROLE_ID
        };
        
        modelBuilder.Entity<User>().HasData(adminUser);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
    }
}