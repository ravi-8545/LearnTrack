using System.ComponentModel.DataAnnotations.Schema;

namespace LearnTrack.Core.Entities;

[Table("courses")]
public class Course
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("courseproviderid")]
    public Guid CourseProviderId { get; set; }

    [Column("coursecategoryid")]
    public Guid CourseCategoryId { get; set; }

    [Column("isactive")]
    public bool IsActive { get; set; } = true;

    [Column("createdby")]
    public Guid CreatedBy { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}