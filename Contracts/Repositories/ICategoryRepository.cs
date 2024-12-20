using Contracts.DTOs;

namespace Contracts.Repositories;

public interface ICategoryRepository
{
    Task<CategoryDto> Create(CategoryDto category);
    Task<CategoryDto?> Get(Guid id);
    Task<bool> IsExists(string name);
    Task<IEnumerable<CategoryDto>> GetAllWithNotes();
    Task<IEnumerable<CategoryDto>> GetAll();
    Task<CategoryDto> Remove(Guid id);
}