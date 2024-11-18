namespace Contracts.DTOs;

public class NoteDto 
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}