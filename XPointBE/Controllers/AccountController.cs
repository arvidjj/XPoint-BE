using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XPointBE.Dtos.User;
using XPointBE.Dtos.User.Auth;
using XPointBE.Models.Usuarios;
using XPointBE.Services.Interfaces;

namespace XPointBE.Controllers;

[Route("api/cuenta")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;
    //logger
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, ILogger<AccountController> logger) 
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _logger = logger;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            
            //get role
            var roles = await _userManager.GetRolesAsync(user);
            
            if (result.Succeeded)
            {
                return Ok(new NewUserDto
                {
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user),
                    Role = roles.FirstOrDefault() ?? "User" // Default to "User" if no roles are assigned
                });
            }
            else
            {
                return Unauthorized("Invalid email or password.");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("whoami")]
    public async Task<IActionResult> WhoAmI([FromBody] WhoAmIRequest whoAmIDto)
    {
        try
        {
            String token = whoAmIDto.Token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }
            var user = await _tokenService.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized("Invalid token.");
            }
            
            return Ok(new WhoAmIResponse 
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Role = user.Role
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("registrar")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var appUser = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Nombre = registerDto.Nombre,
            };
            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        Nombre = appUser.Nombre,
                        Email = appUser.Email,
                        Token = await _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
        
    }
    
}