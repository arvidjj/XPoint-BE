using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
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
        var users = _context.Users.ToList();

        if (users.Count != 0) return Ok(users);
        _logger.LogWarning("No users found");
        return NotFound("No users found");

    }
    
    [HttpGet("{id:int}", Name = "GetUserById")]
    public IActionResult Get([FromRoute] int id)
    {
        _logger.LogInformation($"Fetching user with ID: {id}");
        var user = _context.Users.Find(id);

        if (user != null) return Ok(user);
        
        _logger.LogWarning("User with ID: {Id} not found", id);
        return NotFound();

    }
}