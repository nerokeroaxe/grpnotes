using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDatabase _context;

    public CategoryRepository(IDbContextFactory<AppDatabase> factory)
    {
        _context = factory.CreateDbContext();
    }

    public async Task<CategoryDto> Create(CategoryDto category)
    {
        var result = await _context.Categories.AddAsync(category.ToCategory());
        await _context.SaveChangesAsync();

        return result.Entity.ToCategoryDto();

    }

    public async Task<CategoryDto?> Get(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        return category?.ToCategoryDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _context.Categories.Select(x => x.ToCategoryDto()).ToListAsync();
    }

    public async Task<bool> IsExists(string name)
    {
        return await _context.Categories.AnyAsync(x => x.Name == name);
    }

    public async Task<CategoryDto> Remove(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category!);
        await _context.SaveChangesAsync();

        return category!.ToCategoryDto();
    }
}
