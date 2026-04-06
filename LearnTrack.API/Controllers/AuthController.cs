using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LearnTrack.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
        if (user == null) return Unauthorized("Invalid Credentials");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email) 
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }

    [HttpPost("VerifyInvitation")]
    public async Task<IActionResult> VerifyInvitation([FromQuery] Guid token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.InvitationToken == token);
        
        if (user == null || user.TokenExpiry < DateTime.UtcNow)
            return BadRequest("Invitation link has expired or is invalid.");

        return Ok(new { Message = "Invitation Valid", Email = user.Email });
    }
}

public class LoginRequest { public string Email { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; }