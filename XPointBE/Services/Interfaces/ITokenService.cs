using XPointBE.Dtos.User;
using XPointBE.Models.Usuarios;

namespace XPointBE.Services.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    Task<UserDto> GetUserFromToken(string token);
}