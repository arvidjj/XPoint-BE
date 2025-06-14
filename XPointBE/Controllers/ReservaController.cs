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

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ReservaController : ControllerBase
{
    private readonly ILogger<ReservaController> _logger;
    private readonly IReservaRepository _reservaRepository;
    
    private readonly IServicioRepository _servicioRepository;


    public ReservaController(ILogger<ReservaController> logger, IReservaRepository reservaRepository, IServicioRepository servicioRepository)
    {
        _logger = logger;
        _reservaRepository = reservaRepository;
        _servicioRepository = servicioRepository;
    }
    
    [HttpGet(Name = "GetReservas")]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] ReservasQueryObject reservasQuery)
    {
        
        _logger.LogInformation("Fetching all reservas");
        var reservas = await _reservaRepository.GetAllQueryAsync(reservasQuery);
            
        var reservasDto = reservas.Select(s => s.ToReservaDto()); //select es lo mismo que map en js

        return Ok(reservasDto);
    }
    
    [HttpGet("{id:int}", Name = "GetReservaById")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching reserva with ID: {id}");
        var reserva = await _reservaRepository.GetByIdAsync(id);

        if (reserva != null) return Ok(reserva.ToReservaDto());
        
        _logger.LogWarning("Reserva with ID: {Id} not found", id);
        return NotFound();
    }

    [HttpPost(Name = "CreateReserva")]
    public async Task<IActionResult> Create([FromRoute] int servicioId ,[FromBody] CreateReservaRequestDto reservaDto)
    {
        
        if (!ModelState.IsValid) 
        {
            _logger.LogWarning("Invalid model state for reserva creation");
            return BadRequest(ModelState);
        }
        
        if(!await _servicioRepository.ExistsAsync(servicioId))
        {
            _logger.LogWarning("Servicio with ID: {Id} not found", servicioId);
            return NotFound($"Servicio con ID: {servicioId} no encontrado-");
        }
        
        var reserva = reservaDto.ToReservaFromCreateDto(servicioId);
        _logger.LogInformation("Creating a new reserva");
        
        await _reservaRepository.CreateAsync(reserva);
        _logger.LogInformation("Reserva created successfully with ID: {Id}", reserva.Id);
        return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva.ToReservaDto());
    }
    
    [HttpPut("{id:int}", Name = "UpdateReserva")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReservaRequestDto reservaDto)
    {
        
        if (!ModelState.IsValid) 
        {
            _logger.LogWarning("Invalid model state for reserva update");
            return BadRequest(ModelState);
        }
        
        var reserva = reservaDto.ToReservaFromUpdateDto();
        var reservaModel = await _reservaRepository.UpdateAsync(id, reserva);
        
        if (reservaModel == null)
        {
            _logger.LogWarning("Reserva with ID: {Id} not found", id);
            return NotFound($"Reserva con ID: {id} no encontrado.");
        }

        _logger.LogInformation("Reserva with ID: {Id} updated successfully", id);
        return Ok(reservaModel.ToReservaDto());
    }
    
    [HttpDelete("{id:int}", Name = "DeleteReserva")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var reserva = await _reservaRepository.DeleteAsync(id);

        _logger.LogInformation("Reserva with ID: {Id} deleted successfully", id);
        return NoContent();
    }
    
    
}