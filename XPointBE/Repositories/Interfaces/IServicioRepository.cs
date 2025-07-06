using XPointBE.Models;

namespace XPointBE.Repositories.Interfaces;

public interface IServicioRepository : IRepository<Servicio>
{
    
    Task<bool> AtLeastOneExistsAsync();
}