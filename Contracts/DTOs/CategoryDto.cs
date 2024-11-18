namespace Contracts.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<NoteDto> Notes { get; set; } = new();
}