using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    protected readonly IDbContextFactory<AppDatabase> _factory;

    public NoteRepository(IDbContextFactory<AppDatabase> factory)
    {
        _factory = factory;
    }
    

    public async Task<NoteDto> Create(NoteDto note)
    {
        using var context = _factory.CreateDbContext();
        var result = await context.Notes.AddAsync(note.ToNote());
        await context.SaveChangesAsync();
        return result.Entity.ToNoteDto();
    }

    public async Task<NoteDto?> Get(Guid id)
    {
        using var context = _factory.CreateDbContext();
        var note = await context.Notes.FindAsync(id);
        return note?.ToNoteDto();
    }

    public async Task<NoteDto> Remove(Guid id)
    {
        using var context = _factory.CreateDbContext();
        var note = await context.Notes.FindAsync(id);
        
        context.Notes.Remove(note!);
        await context.SaveChangesAsync();
        return note!.ToNoteDto();
    }
}