using Contracts.DTOs;
using Contracts.Repositories;
using Contracts.Services;
using Contracts.ViewModels;
using Core.Mappers;

namespace Core.Services;

public class CategoryService : ICategoryService
{
    private const int MAX_NAME_LENGTH = 80;
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<CategoryView> Create(CategoryDto category)
    {
        await _validateCategory(category);
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

    public async Task<IEnumerable<CategoryView>> GetAllWithNotes()
    {
        var categories = await _categoryRepo.GetAllWithNotes();
        return categories.Select(x => x.ToCategoryView());
    }

    public async Task<CategoryView> Remove(Guid id)
    {
        var category = await _categoryRepo.Get(id);
        if (category is null)
        {
            throw new ArgumentException("Категория с таким ID не найдена", nameof(id));
        }
        var result = await _categoryRepo.Remove(id);
        return result.ToCategoryView();
    }

    private async Task _validateCategory(CategoryDto category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new ArgumentException("Название категории не должно быть пустым", nameof(category));
        }
        if (category.Name.Length > MAX_NAME_LENGTH)
        {
            throw new ArgumentException($"Название категории не должно быть длинее {MAX_NAME_LENGTH} символов", nameof(category));
        }
        if (await _categoryRepo.IsExists(category.Name))
        {
            throw new ArgumentException("Категория с таким названием уже существует", nameof(category));
        }
    }
}