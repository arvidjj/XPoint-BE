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
            Role = user.Role
        };
    }
}