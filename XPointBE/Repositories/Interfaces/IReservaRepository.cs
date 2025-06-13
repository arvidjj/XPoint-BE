using XPointBE.Helpers;
using XPointBE.Models;

namespace XPointBE.Repositories.Interfaces;


public interface IReservaRepository : IRepository<Reserva>
{
    Task<List<Reserva>> GetAllQueryAsync(ReservasQueryObject reservasQuery);
    
}