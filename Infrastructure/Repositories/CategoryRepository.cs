using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbContextFactory<AppDatabase> _factory;
    public CategoryRepository(IDbContextFactory<AppDatabase> factory)
    {
        _factory = factory;
    }

    public async Task<CategoryDto> Create(CategoryDto category)
    {
        using var context = _factory.CreateDbContext();
        
        var result = await context.Categories.AddAsync(category.ToCategory());
        await context.SaveChangesAsync();

        return result.Entity.ToCategoryDto();

    }

    public async Task<CategoryDto?> Get(Guid id)
    {
        using var context = _factory.CreateDbContext();

        var category = await context.Categories
            .Include(x => x.Notes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return category?.ToCategoryDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllWithNotes()
    {
        using var context = _factory.CreateDbContext();
        return await context.Categories
            .Include(x => x.Notes)
            .Select(x => x.ToCategoryDto())
            .ToListAsync();
    }
    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        using var context = _factory.CreateDbContext();
        return await context.Categories
            .Select(x => x.ToCategoryDto())
            .ToListAsync();
    }

    public async Task<bool> IsExists(string name)
    {
        using var context = _factory.CreateDbContext();
        return await context.Categories.AnyAsync(x => x.Name == name);
    }

    public async Task<CategoryDto> Remove(Guid id)
    {
        using var context = _factory.CreateDbContext();

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        context.Categories.Remove(category!);
        await context.SaveChangesAsync();
        
        return category!.ToCategoryDto();
    }
}
