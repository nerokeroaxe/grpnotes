using Contracts.DTOs;
using Contracts.ViewModels;
using Core.Models;

namespace Core.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this Category category) 
        => new() 
        { 
            Id = category.Id, 
            Name = category.Name,
            Notes = category.Notes.Select(x => x.ToNoteDto()).ToList()
        };
    
    public static Category ToCategory(this CategoryDto categoryDto) 
        => new() 
        { 
            Id = categoryDto.Id, 
            Name = categoryDto.Name 
        };

    public static CategoryView ToCategoryView(this CategoryDto category)
        => new() 
        { 
            Id = category.Id, 
            Name = category.Name,
            Notes = category.Notes.Select(x => x.ToNoteView()).ToList()
        };
}