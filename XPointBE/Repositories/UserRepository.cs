using Microsoft.EntityFrameworkCore;
using XPointBE.Data;
using XPointBE.Models;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null) return null;

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Telefono = user.Telefono;
        existingUser.PasswordHash = user.PasswordHash; // Ensure password is hashed before saving
        existingUser.Role = user.Role;
        
        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }
}