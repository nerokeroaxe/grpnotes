using Contracts.DTOs;
using Contracts.Repositories;
using Contracts.Services;
using Contracts.ViewModels;
using Core.Mappers;

namespace Core.Services;

public class NoteService : INoteService
{
    private const int MAX_CONTENT_LENGTH = 200;
    private readonly INoteRepository _noteRepo;

    public NoteService(INoteRepository noteRepo)
    {
        _noteRepo = noteRepo;
    }
    public async Task<NoteView> Create(NoteDto note)
    {
        _validateNote(note);
        return (await _noteRepo.Create(note)).ToNoteView();
    }

    public async Task<NoteView?> Get(Guid id)
    {
        return (await _noteRepo.Get(id))?.ToNoteView();
    }

    public async Task<NoteView> Remove(Guid id)
    {
        if (await _noteRepo.Get(id) is null)
        {
            throw new ArgumentException("Note not found", nameof(id));
        }
        return (await _noteRepo.Remove(id)).ToNoteView();
    }

    private void _validateNote(NoteDto note)
    {
        if (note.CategoryId == Guid.Empty) 
        {
            throw new ArgumentException("Category id cannot be empty", nameof(note));
        }
        if (string.IsNullOrWhiteSpace(note.Content))
        {
            throw new ArgumentException("Content cannot be empty", nameof(note));
        }
        if (note.Content.Length > MAX_CONTENT_LENGTH)
        {
            throw new ArgumentException($"Content cannot be longer than {MAX_CONTENT_LENGTH} characters", nameof(note));
        }
    }
}