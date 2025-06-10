using XPointBE.Dtos.Reserva;
using XPointBE.Models;

namespace XPointBE.Interfaces;


public interface IReservaRepository
{
    Task<List<Reserva>> GetAllAsync();
    Task<Reserva?> GetByIdAsync(int id); //has ? because it can return null
    Task<Reserva> CreateAsync(Reserva reserva);
    Task<Reserva?> UpdateAsync(Reserva reserva);
    Task<bool> DeleteAsync(int id);
    
}