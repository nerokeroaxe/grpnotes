using Contracts.DTOs;
using Contracts.Repositories;
using Core.Mappers;
using Core.Models;
using Tests.Fakes.Database;

namespace Tests.Fakes.Repositories;

internal class FakeNoteRepo : INoteRepository
{
    public async Task<NoteDto> Create(NoteDto note)
    {
        var noteEntity = note.ToNote();
        noteEntity.Id = Guid.NewGuid();
        await Task.Run(() => FakeDb.Notes.Add(noteEntity));
        return noteEntity.ToNoteDto();
    }

    public async Task<NoteDto?> Get(Guid id)
    {
        var note = await Task.Run(() => FakeDb.Notes.Find(x => x.Id == id));
        return note?.ToNoteDto();
    }

    public async Task<NoteDto> Remove(Guid id)
    {
        var note = await Task.Run(() => FakeDb.Notes.Find(x => x.Id == id));
        await Task.Run(() => FakeDb.Notes.Remove(note));
        return note.ToNoteDto();
    }
}