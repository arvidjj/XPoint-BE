using Newtonsoft.Json;
using XPointBE.Dtos.Ciudad;
using XPointBE.Mappers;
using XPointBE.Models;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Services;

public class CiudadService : ICiudadService
{

    private HttpClient _httpClient;
    private IConfiguration _config;
    public CiudadService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<IEnumerable<Departamento>> GetDepartamentosAsync()
    {
        try
        {
            var result = await _httpClient.GetAsync("https://api.delpi.dev/api/departamentos/");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var departamentosResponse = JsonConvert.DeserializeObject<DepartamentoResponse>(content);
                return departamentosResponse?.data ?? Enumerable.Empty<Departamento>();
            }
            else
            {
                throw new Exception($"Error fetching departamentos: {result.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Departamento>();
        }
    }
    
    public async Task<IEnumerable<Ciudad>> GetCiudadByDepartamentoIdAsync(int id)
    {
        try
        {
            var result = await _httpClient.GetAsync($"https://api.delpi.dev/api/ciudades/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var ciudades = JsonConvert.DeserializeObject<List<Ciudad>>(content);
                return ciudades ?? Enumerable.Empty<Ciudad>();
            }
            else
            {
                throw new Exception($"Error fetching ciudades: {result.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Ciudad>();
        }
    }


    public async Task<IEnumerable<Ciudad>> GetCiudadesAsync()
    {
        try
        {
            var result = await _httpClient.GetAsync("https://api.delpi.dev/api/ciudades/");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var ciudadesResponse = JsonConvert.DeserializeObject<CiudadResponse>(content);
                return ciudadesResponse?.data ?? Enumerable.Empty<Ciudad>();
            }
            else
            {
                throw new Exception($"Error fetching ciudades: {result.ReasonPhrase}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Ciudad>();
        }
    }

}