using Contracts.DTOs;
using Contracts.ViewModels;

namespace Contracts.Services;

public interface ICategoryService 
{
    Task<CategoryView> Create(CategoryDto category);
    Task<CategoryView?> Get(Guid id);
    Task<IEnumerable<CategoryView>> GetAll();
    Task<CategoryView> Remove(Guid id);
}