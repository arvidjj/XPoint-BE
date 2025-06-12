using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public class User : ModelAuditable
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Phone]
    public string? Telefono { get; set; }
    
    public UserRoleEnum Role { get; set; } = UserRoleEnum.User;
    
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public DateTime? UltimaConexion { get; set; }
    
    public ICollection<Reserva> Reservaciones { get; set; }
}