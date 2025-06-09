using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using XPointBE.Dtos.Reserva;
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

    public ReservaController(ILogger<ReservaController> logger, ApplicationDBContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet(Name = "GetReservas")]
    public IActionResult Get()
    {
        _logger.LogInformation("Fetching all reservas");
        var reservas = _context.Reservas.ToList()
            .Select(s => s.ToReservaDto()); //select es lo mismo que map en js

        return Ok(reservas);
    }
    
    [HttpGet("{id:int}", Name = "GetReservaById")]
    public IActionResult Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching reserva with ID: {id}");
        var reserva = _context.Reservas.Find(id);

        if (reserva != null) return Ok(reserva.ToReservaDto());
        
        _logger.LogWarning("Reserva with ID: {Id} not found", id);
        return NotFound();
    }

    [HttpPost(Name = "CreateReserva")]
    public IActionResult Create([FromBody] CreateReservaRequestDto reservaDto)
    {
        var reserva = reservaDto.ToReservaFromCreateDTO();
        _logger.LogInformation("Creating a new reserva");
        if (reserva == null)
        {
            _logger.LogWarning("Reserva data is null");
            return BadRequest("Reserva data cannot be null");
        }
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
        _logger.LogInformation("Reserva created successfully with ID: {Id}", reserva.Id);
        return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva.ToReservaDto());
    }
    
}