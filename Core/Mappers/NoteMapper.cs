using Contracts.DTOs;
using Contracts.ViewModels;
using Core.Models;

namespace Core.Mappers;

public static class NoteMapper
{
    public static Note ToNote(this NoteDto noteDto) 
        => new() 
        { 
            Id = noteDto.Id, 
            CategoryId = noteDto.CategoryId, 
            Content = noteDto.Content
        };
    public static NoteView ToNoteView(this NoteDto note)
        => new() 
        { 
            Id = note.Id, 
            Content = note.Content, 
            Category = note.Category?.Name ?? string.Empty 
        };
    public static NoteDto ToNoteDto(this Note note)
        => new() 
        { 
            Id = note.Id, 
            CategoryId = note.CategoryId, 
            Content = note.Content,
            Category = note.Category?.ToCategoryDto()
        };
}