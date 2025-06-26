using System.ComponentModel.DataAnnotations;
using XPointBE.Models;

namespace XPointBE.Dtos.User;

public class UpdateUserRequestDto
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
    public UserRoleEnum Role { get; set; }
    
    [Phone]
    [StringLength(20)]
    public string? Telefono { get; set; }
    
    public bool Activo { get; set; } 
}