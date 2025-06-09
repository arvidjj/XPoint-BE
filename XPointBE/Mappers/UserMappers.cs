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
            Role = user.Role.ToString()
        };
    }
    
    public static User ToUser (this CreateUserRequestDto createUserRequestDto)
    {
        return new User
        {
            Name = createUserRequestDto.Name,
            Email = createUserRequestDto.Email,
            PasswordHash = createUserRequestDto.Password, // se necesita hashear en control
            Role = createUserRequestDto.Role
        };
    }
}