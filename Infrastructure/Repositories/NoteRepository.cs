using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    protected readonly AppDatabase _context;

    public NoteRepository(IDbContextFactory<AppDatabase> factory)
    {
        _context = factory.CreateDbContext();
    }
    

    public async Task<NoteDto> Create(NoteDto note)
    {
        var result = await _context.Notes.AddAsync(note.ToNote());
        await _context.SaveChangesAsync();
        return result.Entity.ToNoteDto();
    }

    public async Task<NoteDto?> Get(Guid id)
    {
        var note = await _context.Notes.FindAsync(id);
        return note?.ToNoteDto();
    }

    public async Task<NoteDto> Remove(Guid id)
    {
        var note = await _context.Notes.FindAsync(id);
        _context.Notes.Remove(note!);
        await _context.SaveChangesAsync();
        return note!.ToNoteDto();
    }
}