using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using XPointBE.Dtos.User;
using XPointBE.Models;
using XPointBE.Models.Usuarios;
using XPointBE.Repositories.Interfaces;
using XPointBE.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace XPointBE.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    private readonly IUserRepository _userRepo;
    private readonly UserManager<User> _userManager;
    
    public TokenService(IConfiguration config, IUserRepository userRepo, UserManager<User> userManager)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"] ?? throw new ArgumentNullException("JWT Key is not configured.")));
        _userRepo = userRepo;
        _userManager = userManager;
    }
    
    public async Task<string> CreateToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Nombre),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "User")
        };
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public async Task<UserDto> GetUserFromToken(string token)
    {
        try 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            
            if (jwtToken == null)
            {
                return null;
            }
            
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value;
            if (userId == null)
            {
                return null;
            }

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            
            return new UserDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Telefono = user.Telefono,
                Role = role
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
}