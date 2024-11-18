using Contracts.DTOs;
using Contracts.Repositories;
using Contracts.Services;
using Contracts.ViewModels;
using Core.Mappers;

namespace Core.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<CategoryView> Create(CategoryDto category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new ArgumentException("Category name cannot be empty", nameof(category));
        }
        if (await _categoryRepo.IsExists(category.Name))
        {
            throw new ArgumentException("Category already exists", nameof(category));
        }
        var result = await _categoryRepo.Create(category);
        return result.ToCategoryView();
    }

    public async Task<CategoryView?> Get(Guid id)
    {
        var category = await _categoryRepo.Get(id);
        return category?.ToCategoryView();
    }

    public async Task<IEnumerable<CategoryView>> GetAll()
    {
        var categories = await _categoryRepo.GetAll();
        return categories.Select(x => x.ToCategoryView());
    }

    public async Task<CategoryView> Remove(Guid id)
    {
        var category = await _categoryRepo.Get(id);
        if (category is null)
        {
            throw new ArgumentException("Category not found", nameof(id));
        }
        var result = await _categoryRepo.Remove(id);
        return result.ToCategoryView();
    }
}