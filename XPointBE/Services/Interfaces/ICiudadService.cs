using XPointBE.Models;

namespace XPointBE.Repositories.Interfaces;

public interface ICiudadService
{
    Task <IEnumerable<Departamento>> GetDepartamentosAsync();
    
    Task<IEnumerable<Ciudad>> GetCiudadByDepartamentoIdAsync(int id);
    Task<IEnumerable<Ciudad>> GetCiudadesAsync();
}