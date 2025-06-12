using XPointBE.Dtos.User;
using XPointBE.Models;

namespace XPointBE.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto (this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Telefono = user.Telefono,
            Role = user.Role
        };
    }
    
    public static User ToUser (this CreateUserRequestDto createUserRequestDto)
    {
        return new User
        {
            Name = createUserRequestDto.Name,
            Email = createUserRequestDto.Email,
            PasswordHash = createUserRequestDto.Password, //TODO: Hash the password con spring security, servicio de autenticación y utorización
            Role = createUserRequestDto.Role,
            Telefono = createUserRequestDto.Telefono
        };
    }
}