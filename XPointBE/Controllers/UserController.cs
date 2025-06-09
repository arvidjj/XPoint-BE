using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using XPointBE.Mappers;
using XPointBE.Models;

namespace XPointBE.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDBContext _context;

    public UserController(ILogger<UserController> logger, ApplicationDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
        _logger.LogInformation("Fetching all users");
        var users = _context.Users.ToList()
            .Select(s => s.ToUserDto()); //select es lo mismo que map en js

        return Ok(users);
    }
    
    [HttpGet("{id:int}", Name = "GetUserById")]
    public IActionResult Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching user with ID: {id}");
        var user = _context.Users.Find(id);

        if (user != null) return Ok(user.ToUserDto());
        
        _logger.LogWarning("User with ID: {Id} not found", id);
        return NotFound();

    }
    
    [HttpPost(Name = "CreateUser")]
    public IActionResult Post([FromBody] User user)
    {
        _logger.LogInformation("Creating a new user");
        if (user == null)
        {
            _logger.LogWarning("User data is null");
            return BadRequest("User data cannot be null");
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        _logger.LogInformation($"User created with ID: {user.Id}");
        return CreatedAtRoute("GetUserById", new { id = user.Id }, user.ToUserDto());
    }
}