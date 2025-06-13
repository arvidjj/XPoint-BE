using XPointBE.Models;

namespace XPointBE.Dtos.User;

public class UserDto : BaseDto
{
    
    public new string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoleEnum Role { get; set; }
    public string? Telefono { get; set; }
    public DateTime? UltimaConexion { get; set; }
    public bool Activo { get; set; }
}