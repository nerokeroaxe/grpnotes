using Contracts.DTOs;
using Contracts.Services;
using Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Pages;

public class CategoryModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICategoryService _categoryService;
    public List<CategoryView> Categories { get; set; } = new();
    
    [BindProperty]
    public CategoryDto Category { get; set; } = new();
    
    public CategoryModel(ILogger<IndexModel> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> OnGetAsync()
    {   
        try
        {
            Categories = (await _categoryService.GetAll()).ToList();
            _logger.LogInformation("Categories: {Categories}", Categories.Count);
        } 
        catch(Exception e)
        {
            _logger.LogError("Error on get categories: " + e.Message);
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        _logger.LogInformation("Create category on page: {Category}", Category.Name);
        try 
        {
            await _categoryService.Create(Category);
        }
        catch (Exception e)
        {
            _logger.LogError("Error on create category: " + e.Message);
        }
        
        return RedirectToPage("Category");
    }

    public async Task<IActionResult> OnPostRemove(Guid id)
    {
        _logger.LogInformation("Remove category on page id: {id}", id);
        try 
        {
            await _categoryService.Remove(id);
        }
        catch (Exception e)
        {
            _logger.LogError("Error on remove category: " + e.Message);
        }

        return RedirectToPage("Category");
    }
}
