using XPointBE.Models;

namespace XPointBE.Repositories.Interfaces;

public interface IRepository<T> where T : class, IBaseModel
    {
        
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        
    }

