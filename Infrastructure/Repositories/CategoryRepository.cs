using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbContextFactory<AppDatabase> _context;
    public CategoryRepository(IDbContextFactory<AppDatabase> factory)
    {
        _context = factory;
    }

    public async Task<CategoryDto> Create(CategoryDto category)
    {
        var context = _context.CreateDbContext();
        
        var result = await context.Categories.AddAsync(category.ToCategory());
        await context.SaveChangesAsync();

        return result.Entity.ToCategoryDto();

    }

    public async Task<CategoryDto?> Get(Guid id)
    {
        var context = _context.CreateDbContext();

        var category = await context.Categories
            .Include(x => x.Notes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return category?.ToCategoryDto();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllWithNotes()
    {
        var context = _context.CreateDbContext();
        return await context.Categories
            .Include(x => x.Notes)
            .Select(x => x.ToCategoryDto())
            .ToListAsync();
    }
    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var context = _context.CreateDbContext();
        return await context.Categories
            .Select(x => x.ToCategoryDto())
            .ToListAsync();
    }

    public async Task<bool> IsExists(string name)
    {
        var context = _context.CreateDbContext();
        return await context.Categories.AnyAsync(x => x.Name == name);
    }

    public async Task<CategoryDto> Remove(Guid id)
    {
        var context = _context.CreateDbContext();

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        context.Categories.Remove(category!);
        await context.SaveChangesAsync();

        return category!.ToCategoryDto();
    }
}
