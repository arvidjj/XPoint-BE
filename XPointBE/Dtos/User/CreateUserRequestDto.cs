using System.ComponentModel.DataAnnotations;
using XPointBE.Models;

namespace XPointBE.Dtos.User;

public class CreateUserRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    [EnumDataType(typeof(UserRole))]
    public UserRole Role { get; set; } = UserRole.User;
    
}