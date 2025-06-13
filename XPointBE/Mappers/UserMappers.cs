using XPointBE.Dtos.User;
using XPointBE.Models;
using XPointBE.Models.Usuarios;

namespace XPointBE.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto (this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Nombre,
            Email = user.Email,
            Telefono = user.Telefono,
        };
    }
    
    public static User ToUser (this CreateUserRequestDto createUserRequestDto)
    {
        return new User
        {
            Nombre = createUserRequestDto.Name,
            Email = createUserRequestDto.Email,
            PasswordHash = createUserRequestDto.Password, //TODO: Hash the password con spring security, servicio de autenticación y utorización
            Telefono = createUserRequestDto.Telefono
        };
    }
}