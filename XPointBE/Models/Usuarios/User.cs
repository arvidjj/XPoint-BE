using Microsoft.AspNetCore.Identity;

namespace XPointBE.Models.Usuarios;

public class User : IdentityUser
{
    
    public string Nombre { get; set; } = string.Empty;
    
    public string? Telefono
    {
        get => PhoneNumber;
        set => PhoneNumber = value;
    }
    
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public DateTime? UltimaConexion { get; set; }
    
    public ICollection<Reserva> Reservaciones { get; set; }
    
    // auditable
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}