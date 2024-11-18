using Contracts.DTOs;
using Contracts.Repositories;
using Contracts.Services;
using Core.Services;
using Moq;
using Tests.Fakes.Database;
using Tests.Fakes.Repositories;

namespace Tests.Integration;

internal class NoteCategoryServiceTest
{
    private ICategoryService _categoryService;
    private INoteService _noteService;

    [SetUp]
    public void Setup()
    {
        _categoryService = new CategoryService(new FakeCategoryRepo());
        _noteService = new NoteService(new FakeNoteRepo());
    }

    [TearDown]
    public void TearDown()
    {
        FakeDb.ClearDb();
    }

    [Test]
    public async Task GetCategory_ThenReturnCategoryWithNotes()
    {
        var category = new CategoryDto() { Name = "test" };
        var savedCategory = await _categoryService.Create(category);
        var notes = new List<NoteDto>()
        {
            new NoteDto() { CategoryId = savedCategory.Id, Content = "test1" },
            new NoteDto() { CategoryId = savedCategory.Id, Content = "test2" },
        };
        foreach (var note in notes)
        {
            await _noteService.Create(note);
        }

        var categoryWithNotes = await _categoryService.Get(savedCategory.Id);

        Assert.AreEqual(categoryWithNotes.Notes.Count(), 2);
    }

    [Test]
    public async Task RemoveCategory_ThenRemoveCategoryWithNotes()
    {
        var category = new CategoryDto() { Name = "test" };
        var savedCategory = await _categoryService.Create(category);
        var notes = new List<NoteDto>()
        {
            new NoteDto() { CategoryId = savedCategory.Id, Content = "test1" },
            new NoteDto() { CategoryId = savedCategory.Id, Content = "test2" },
        };
        foreach (var note in notes)
        {
            await _noteService.Create(note);
        }

        await _categoryService.Remove(savedCategory.Id);
        var categoryWithNotes = await _categoryService.Get(savedCategory.Id);
        
        Assert.IsNull(categoryWithNotes);
        Assert.AreEqual(FakeDb.Notes.Count(), 0);
    }
}