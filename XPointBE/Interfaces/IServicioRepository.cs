using XPointBE.Models;

namespace XPointBE.Interfaces;

public interface IServicioRepository
{
    Task<List<Servicio>> GetAllAsync();
    Task<Servicio?> GetByIdAsync(int id); //has ? because it can return null
    Task<Servicio> CreateAsync(Servicio servicio);
    Task<Servicio?> UpdateAsync(Servicio servicio);
    Task<bool> DeleteAsync(int id);
}