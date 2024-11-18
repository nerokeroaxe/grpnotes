namespace Contracts.ViewModels;

public class CategoryView
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<NoteView> Notes { get; set; } = new();
}