namespace Core.Models;

public class Note
{
    public Guid Id { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string Content { get; set; } = string.Empty;
}