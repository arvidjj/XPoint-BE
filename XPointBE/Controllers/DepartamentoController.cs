using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using XPointBE.Data;
using XPointBE.Dtos.Reserva;
using XPointBE.Helpers;
using XPointBE.Mappers;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class DepartamentoController : ControllerBase
{
    private readonly ILogger<DepartamentoController> _logger;
    private readonly ICiudadService _ciudadService;

    public DepartamentoController(ILogger<DepartamentoController> logger, ICiudadService ciudadService)
    {
        _logger = logger;
        _ciudadService = ciudadService;
    }
    
    [HttpGet(Name = "GetDepartamentos")]
    public async Task<IActionResult> GetDepartamentos()
    {
        
        _logger.LogInformation("Fetching all departamentos");
        var ciudades = await _ciudadService.GetDepartamentosAsync();
            
        var ciudadesDto = ciudades.Select(s => s.MapToDepartamentoDto()); //select es lo mismo que map en js

        return Ok(ciudadesDto);
    }
    
    [HttpGet("ciudades", Name = "GetCiudades")]
    public async Task<IActionResult> GetCiudades()
    {
        
        _logger.LogInformation("Fetching all ciudades");
        var ciudades = await _ciudadService.GetCiudadesAsync();
            
        var ciudadesDto = ciudades.Select(s => s.MapToCiudadDto()); //select es lo mismo que map en js

        return Ok(ciudadesDto);
    }
    
    [HttpGet("ciudades/{id:int}", Name = "GetCiudadByDepartamento")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching ciudades ID: {id}");
        var ciudades = await _ciudadService.GetCiudadByDepartamentoIdAsync(id);

        var ciudadesDto = ciudades.Select(s => s.MapToCiudadDto()); //select es lo mismo que map en js
        
        return Ok(ciudadesDto);
    }
    
    
}