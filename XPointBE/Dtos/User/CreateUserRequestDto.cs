using System.ComponentModel.DataAnnotations;
using XPointBE.Models;

namespace XPointBE.Dtos.User;

public class CreateUserRequestDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    
    public UserRoleEnum Role { get; set; } = UserRoleEnum.User;
    
    [Phone]
    [StringLength(20)]
    public string? Telefono { get; set; }
    
}