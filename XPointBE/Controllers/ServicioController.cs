using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using XPointBE.Dtos.Reserva;
using XPointBE.Dtos.Servicio;
using XPointBE.Interfaces;
using XPointBE.Mappers;

namespace XPointBE.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ServicioController : ControllerBase
{
    private readonly ILogger<ServicioController> _logger;
    private readonly IServicioRepository _servicioRepository;


    public ServicioController(ILogger<ServicioController> logger, IServicioRepository servicioRepository)
    {
        _logger = logger;
        _servicioRepository = servicioRepository;
    }
    
    [HttpGet(Name = "GetServicios")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching all Servicios");
        var servicios = await _servicioRepository.GetAllAsync();
        var serviciosDto = servicios.Select(s => s.ToServicioDto()); //select es lo mismo que map en js

        return Ok(serviciosDto);
    }
    
    [HttpGet("{id:int}", Name = "GetServicioById")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching Servicio with ID: {id}");
        var servicio = await _servicioRepository.GetByIdAsync(id);

        if (servicio != null) return Ok(servicio.ToServicioDto());
        
        _logger.LogWarning("Servicio with ID: {Id} not found", id);
        return NotFound();
    }

    [HttpPost(Name = "CreateServicio")]
    public async Task<IActionResult> Create([FromBody] CreateServicioRequestDto servicioDto)
    {
        var servicio = servicioDto.ToServicioFromCreateDTO();
        _logger.LogInformation("Creating a new Servicio");
        
        await _servicioRepository.CreateAsync(servicio);
        _logger.LogInformation("Servicio created successfully with ID: {Id}", servicio.Id);
        return CreatedAtAction(nameof(Get), new { id = servicio.Id }, servicio.ToServicioDto());
    }
    
    [HttpPut("{id:int}", Name = "UpdateServicio")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateServicioRequestDto servicioDto)
    {
        var servicio = servicioDto.ToServicioFromUpdateDTO(id);
        var servicioModel = await _servicioRepository.UpdateAsync(servicio);
        
        if (servicioModel == null)
        {
            _logger.LogWarning("Servicio with ID: {Id} not found for update", id);
            return NotFound();
        }
        
        _logger.LogInformation("Servicio with ID: {Id} updated successfully", id);
        return Ok(servicioModel.ToServicioDto());
    }
    
    [HttpDelete("{id:int}", Name = "DeleteServicio")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var servicio = await _servicioRepository.DeleteAsync(id);

        _logger.LogInformation("Servicio with ID: {Id} deleted successfully", id);
        return NoContent();
    }
    
    
}