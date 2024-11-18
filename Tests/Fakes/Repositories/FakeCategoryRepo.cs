using Contracts.DTOs;
using Core.Models;
using Contracts.Repositories;
using Core.Mappers;
using Tests.Fakes.Database;

namespace Tests.Fakes.Repositories;

internal class FakeCategoryRepo : ICategoryRepository
{   
    public async Task<CategoryDto> Create(CategoryDto category)
    {
        var categ = category.ToCategory();
        categ.Id = Guid.NewGuid();
        await Task.Run(() => FakeDb.Categories.Add(categ));

        return categ.ToCategoryDto();
    }

    public async Task<CategoryDto?> Get(Guid id)
    {
        var category = await Task.Run(() => FakeDb.Categories.Find(x => x.Id == id));
        if (category != null) 
        {
            var notes = FakeDb.Notes.Where(x => x.CategoryId == id);
            category.Notes = notes.ToList();
        } 
        return category?.ToCategoryDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllWithNotes()
    {
        return await Task.Run(() => FakeDb.Categories.Select(x => x.ToCategoryDto()));
    }

    public async Task<bool> IsExists(string name)
    {
        return await Task.Run(() => FakeDb.Categories.Any(x => x.Name == name));
    }

    public async Task<CategoryDto> Remove(Guid id)
    {
        return await Task.Run(() => 
        {
            var category = FakeDb.Categories.Find(x => x.Id == id);
            FakeDb.Categories.Remove(category);
            FakeDb.Notes.RemoveAll(x => x.CategoryId == id);
            return category.ToCategoryDto();
        });
    }
}