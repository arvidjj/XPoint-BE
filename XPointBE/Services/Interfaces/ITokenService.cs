using XPointBE.Models.Usuarios;

namespace XPointBE.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}