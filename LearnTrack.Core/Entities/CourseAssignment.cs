namespace LearnTrack.Core.Entities;

public class CourseAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public Guid CourseId { get; set; }
    public decimal ProgressPercentage { get; set; }
    public string Status { get; set; } = "Assigned";
    public DateTime? StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}