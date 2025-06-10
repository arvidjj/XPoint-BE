using XPointBE.Models;

namespace XPointBE.Interfaces;


public interface IReservaRepository
{
    Task<List<Reserva>> GetAllAsync();
}