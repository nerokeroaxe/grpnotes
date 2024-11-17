using Contracts.DTOs;
using Contracts.Services;
using Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class CategoryModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICategoryService _categoryService;
    public List<CategoryView> Categories { get; set; }
    
    [BindProperty]
    public CategoryDto Category { get; set; }
    
    public CategoryModel(ILogger<IndexModel> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    public async Task OnGet()
    {
        Categories = (await _categoryService.GetAll()).ToList();
    }

    public async Task<IActionResult> OnPost()
    {
        await _categoryService.Create(Category);
        
        return Page();
    }

    public async Task<IActionResult> OnPostRemove(Guid id)
    {
        await _categoryService.Remove(id);
        
        return Page();
    }
}
