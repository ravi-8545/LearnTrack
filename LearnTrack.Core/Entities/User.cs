namespace LearnTrack.Core.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Using UUID
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}