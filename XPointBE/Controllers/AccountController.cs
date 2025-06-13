using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XPointBE.Dtos.User.Auth;
using XPointBE.Models.Usuarios;

namespace XPointBE.Controllers;

[Route("api/cuenta")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly UserManager<User> _userManager;
    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
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
                    return Ok(new { message = "Usuario registrado exitosamente." });
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