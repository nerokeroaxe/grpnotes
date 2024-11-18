using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Core.Models;

namespace Tests.Fakes.Repositories;

internal class FakeNoteRepo : INoteRepository
{
    private List<Note> _notes = new();
    public async Task<NoteDto> Create(NoteDto note)
    {
        var noteEntity = note.ToNote();
        noteEntity.Id = Guid.NewGuid();
        await Task.Run(() => _notes.Add(noteEntity));
        return noteEntity.ToNoteDto();
    }

    public async Task<NoteDto?> Get(Guid id)
    {
        var note = await Task.Run(() => _notes.Find(x => x.Id == id));
        return note?.ToNoteDto();
    }

    public async Task<NoteDto> Remove(Guid id)
    {
        var note = await Task.Run(() => _notes.Find(x => x.Id == id));
        await Task.Run(() => _notes.Remove(note));
        return note.ToNoteDto();
    }
}