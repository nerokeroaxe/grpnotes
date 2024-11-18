namespace Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Note> Notes { get; set; } = new();
}