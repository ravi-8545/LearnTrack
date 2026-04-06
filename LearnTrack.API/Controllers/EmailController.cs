using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnTrack.Infrastructure.Data;
using LearnTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnTrack.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmailController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("TriggerInvitation")]
    public async Task<IActionResult> TriggerInvitation(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound("User not found");

        // Set token and 24-hour expiry
        user.InvitationToken = Guid.NewGuid();
        user.TokenExpiry = DateTime.UtcNow.AddHours(24);

        await _context.SaveChangesAsync();

        // In a real professional app, you'd call an SMTP service here.
        // For now, we return the link for the frontend to use.
        var inviteLink = $"https://learntrack.com/register?token={user.InvitationToken}";
        
        return Ok(new { Message = "Invitation Token Generated", Link = inviteLink });
    }
}