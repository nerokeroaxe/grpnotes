using Contracts.DTOs;
using Contracts.ViewModels;

namespace Contracts.Services;

public interface INoteService
{
    Task<NoteView> Create(NoteDto note);
    Task<NoteView?> Get(Guid id);
    Task<NoteView> Remove(Guid id);
}