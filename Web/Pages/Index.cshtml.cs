using Contracts.DTOs;
using Contracts.Services;
using Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICategoryService _categoryService;
    private readonly INoteService _noteService;

    public List<CategoryView> Categories { get; set; } = new();
    
    [BindProperty]
    public NoteDto Note { get; set; } = new();

    public IndexModel(ILogger<IndexModel> logger, ICategoryService categoryService, 
        INoteService noteService)
    {
        _logger = logger;
        _categoryService = categoryService;
        _noteService = noteService;
    }

    public async Task<IActionResult> OnGet()
    {
        try 
        {
            Categories = (await _categoryService.GetAllWithNotes()).ToList();
            _logger.LogInformation("Categories at note page: {Categories}", Categories.Count);
        }
        catch (Exception e)
        {
            _logger.LogError("Error on get categories at note page: " + e.Message);
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    { 
        _logger.LogInformation("Create note on page: CategoryId = {Note}", Note.CategoryId);
        try 
        {
            await _noteService.Create(Note);
        }
        catch (Exception e)
        {
            _logger.LogError("Error on create note: " + e.Message);
        }
        
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostRemove(Guid id)
    {
        _logger.LogInformation("Remove note on page id: {id}", id);
        try 
        {
            await _noteService.Remove(id);
        }
        catch (Exception e)
        {
            _logger.LogError("Error on remove note: " + e.Message);
        }
        return RedirectToPage("Index");
    }
}
