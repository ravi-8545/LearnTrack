using System.ComponentModel.DataAnnotations.Schema;

namespace LearnTrack.Core.Entities;

[Table("documents")]
public class Document
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("employeeid")]
    public Guid EmployeeId { get; set; }

    [Column("assignmentid")]
    public Guid AssignmentId { get; set; }

    [Column("filename")]
    public string FileName { get; set; } = string.Empty;

    [Column("filetype")]
    public string FileType { get; set; } = "application/pdf";

    [Column("content")]
    public byte[] Content { get; set; } = null!; // THE BLOB

    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}