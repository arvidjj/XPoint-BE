using XPointBE.Models;

namespace XPointBE.Dtos.User.Auth;

public class WhoAmIResponse
{
    public new string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; }
}