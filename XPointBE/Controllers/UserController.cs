using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using XPointBE.Data;
using XPointBE.Dtos.User;
using XPointBE.Mappers;
using XPointBE.Models;
using XPointBE.Repositories.Interfaces;

namespace XPointBE.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, ApplicationDbContext context, IUserRepository userRepository)
    {
        _logger = logger;
        _context = context;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching all users");

        var users = await _userRepository.GetAllAsync();
        var usersDto = users.Select(s => s.ToUserDto());

        return Ok(usersDto);
    }
    
    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching user with ID: {id}");
        var user = await _userRepository.GetByIdAsync(id);

        if (user != null) return Ok(user.ToUserDto());
        
        _logger.LogWarning("User with ID: {Id} not found", id);
        return NotFound();

    }
    
    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Post([FromBody] CreateUserRequestDto userDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for CreateUser");
            return BadRequest(ModelState);
        }
        
        var user = userDto.ToUser();
        _logger.LogInformation("Creating a new User");
        
        await _userRepository.CreateAsync(user);
        _logger.LogInformation("User created successfully with ID: {Id}", user.Id);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user.ToUserDto());
    }
    
    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        _logger.LogInformation($"Deleting user with ID: {id}");
        var user = await _userRepository.DeleteAsync(id);

        _logger.LogInformation("Usuario con ID: {Id} deleted successfully", id);
        return NoContent();
    }
}