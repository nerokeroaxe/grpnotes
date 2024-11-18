using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    protected readonly IDbContextFactory<AppDatabase> _context;

    public NoteRepository(IDbContextFactory<AppDatabase> factory)
    {
        _context = factory;
    }
    

    public async Task<NoteDto> Create(NoteDto note)
    {
        var context = _context.CreateDbContext();
        var result = await context.Notes.AddAsync(note.ToNote());
        await context.SaveChangesAsync();
        return result.Entity.ToNoteDto();
    }

    public async Task<NoteDto?> Get(Guid id)
    {
        var context = _context.CreateDbContext();
        var note = await context.Notes.FindAsync(id);
        return note?.ToNoteDto();
    }

    public async Task<NoteDto> Remove(Guid id)
    {
        var context = _context.CreateDbContext();
        var note = await context.Notes.FindAsync(id);
        
        context.Notes.Remove(note!);
        await context.SaveChangesAsync();
        return note!.ToNoteDto();
    }
}