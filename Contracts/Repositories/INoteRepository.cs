using Contracts.DTOs;

namespace Contracts.Repositories;

public interface INoteRepository
{
    Task<NoteDto> Create(NoteDto note);
    Task<NoteDto> Get(Guid id);
    Task<NoteDto> Remove(Guid id);
}