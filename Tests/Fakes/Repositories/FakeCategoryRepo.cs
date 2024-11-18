using Contracts.DTOs;
using Core.Models;
using Contracts.Repositories;
using Core.Mappers;

namespace Tests.Fakes.Repositories;

internal class FakeCategoryRepo : ICategoryRepository
{
    private List<Category> _categories = new List<Category>();
    public async Task<CategoryDto> Create(CategoryDto category)
    {
        var categ = category.ToCategory();
        categ.Id = Guid.NewGuid();
        await Task.Run(() => _categories.Add(categ));

        return categ.ToCategoryDto();
    }

    public async Task<CategoryDto?> Get(Guid id)
    {
        var category = await Task.Run(() => _categories.Find(x => x.Id == id));

        return category?.ToCategoryDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await Task.Run(() => _categories.Select(x => x.ToCategoryDto()));
    }

    public async Task<bool> IsExists(string name)
    {
        return await Task.Run(() => _categories.Any(x => x.Name == name));
    }

    public async Task<CategoryDto> Remove(Guid id)
    {
        return await Task.Run(() => 
        {
            var category = _categories.Find(x => x.Id == id);
            _categories.Remove(category);
            return category.ToCategoryDto();
        });
    }
}