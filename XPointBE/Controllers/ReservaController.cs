using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using XPointBE.Dtos.Reserva;
using XPointBE.Interfaces;
using XPointBE.Mappers;

namespace XPointBE.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ReservaController : ControllerBase
{
    private readonly ILogger<ReservaController> _logger;
    private readonly ApplicationDBContext _context;
    private readonly IReservaRepository _reservaRepository;


    public ReservaController(ILogger<ReservaController> logger, ApplicationDBContext context, IReservaRepository reservaRepository)
    {
        _logger = logger;
        _context = context;
        _reservaRepository = reservaRepository;
    }
    
    [HttpGet(Name = "GetReservas")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching all reservas");
        var reservas = await _reservaRepository.GetAllAsync();
            
        var reservasDto = reservas.Select(s => s.ToReservaDto()); //select es lo mismo que map en js

        return Ok(reservasDto);
    }
    
    [HttpGet("{id:int}", Name = "GetReservaById")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching reserva with ID: {id}");
        var reserva = await _context.Reservas.FindAsync(id);

        if (reserva != null) return Ok(reserva.ToReservaDto());
        
        _logger.LogWarning("Reserva with ID: {Id} not found", id);
        return NotFound();
    }

    [HttpPost(Name = "CreateReserva")]
    public async Task<IActionResult> Create([FromBody] CreateReservaRequestDto reservaDto)
    {
        var reserva = reservaDto.ToReservaFromCreateDTO();
        _logger.LogInformation("Creating a new reserva");
        if (reserva == null)
        {
            _logger.LogWarning("Reserva data is null");
            return BadRequest("Reserva data cannot be null");
        }
        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Reserva created successfully with ID: {Id}", reserva.Id);
        return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva.ToReservaDto());
    }
    
    [HttpPut("{id:int}", Name = "UpdateReserva")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReservaRequestDto reservaDto)
    {
        var existingReserva = await _context.Reservas.FindAsync(id);
        if (existingReserva == null)
        {
            _logger.LogWarning("Reserva with ID: {Id} not found for update", id);
            return NotFound();
        }

        existingReserva.Fecha = reservaDto.Fecha;
        existingReserva.UsuarioId = reservaDto.UsuarioId;
        existingReserva.ServicioId = reservaDto.ServicioId;
        existingReserva.Terminada = reservaDto.Terminada;

        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Reserva with ID: {Id} updated successfully", id);
        return Ok(existingReserva.ToReservaDto());
    }
    
    [HttpDelete("{id:int}", Name = "DeleteReserva")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
        {
            _logger.LogWarning("Reserva with ID: {Id} not found for deletion", id);
            return NotFound();
        }

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Reserva with ID: {Id} deleted successfully", id);
        return NoContent();
    }
    
    
}