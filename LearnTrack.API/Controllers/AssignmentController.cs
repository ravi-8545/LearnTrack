using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnTrack.Infrastructure.Data;
using LearnTrack.Core.Entities;
using System.Text.Json;

namespace LearnTrack.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssignmentController(AppDbContext context) => _context = context;

    [HttpPost("Assign")]
    public async Task<IActionResult> AssignCourse([FromBody] CourseAssignment assignment)
    {
        assignment.Id = Guid.NewGuid();
        _context.CourseAssignments.Add(assignment);

        // Audit Log Implementation
        var audit = new AuditLog
        {
            UserId = Guid.Parse(User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value!),
            ActionType = "ASSIGN_COURSE",
            EntityName = "CourseAssignment",
            ChangesJson = JsonSerializer.Serialize(assignment)
        };
        _context.AuditLogs.Add(audit);

        await _context.SaveChangesAsync();
        return Ok(new { Message = "Course Assigned Successfully", Id = assignment.Id });
    }
}