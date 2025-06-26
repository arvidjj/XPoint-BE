using XPointBE.Models;
using XPointBE.Models.Usuarios;

namespace XPointBE.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(string id);
    Task<User> CreateAsync(User entity);
    Task<User?> UpdateAsync(int id, User entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(string id);
}