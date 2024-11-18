namespace Contracts.ViewModels;

public class NoteView
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}