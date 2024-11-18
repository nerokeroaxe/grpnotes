using Contracts.DTOs;
using Contracts.Services;
using Contracts.ViewModels;
using Core.Services;
using Tests.Fakes.Repositories;

namespace Tests.NoteTests;

internal class NoteServiceTests
{
    private INoteService _noteService;
    [SetUp]
    public void Setup()
    {
        _noteService = new NoteService(new FakeNoteRepo());
    }

    [Test]
    public async Task Create_ThenSaveAndReturnNote()
    {
        var note = new NoteDto() { CategoryId = Guid.NewGuid(), Content = "test" };

        var res = await _noteService.Create(note);

        Assert.NotNull(res);
        Assert.NotNull(res.Id);
        Assert.AreEqual(res.Content, note.Content);
    }

    [Test]
    public async Task Create_WhenCategoryNotExists_ThenThrowArgumentException()
    {
        var note = new NoteDto() { CategoryId = Guid.Empty, Content = "test" };

        Assert.ThrowsAsync<ArgumentException>(async () => await _noteService.Create(note));
    }

    [Test]
    public async Task Create_WhenContentIsEmpty_ThenThrowArgumentException()
    {
        var note = new NoteDto() { CategoryId = Guid.NewGuid(), Content = "" };

        Assert.ThrowsAsync<ArgumentException>(async () => await _noteService.Create(note));
    }

    [Test]
    public async Task Create_WhenContentSizeExceedsLimit_ThenThrowArgumentException()
    {
        var limit = 200;
        var note = new NoteDto() { CategoryId = Guid.NewGuid(), Content = new string('*', limit + 1) };

        Assert.ThrowsAsync<ArgumentException>(async () => await _noteService.Create(note));
    }

    [Test]
    public async Task Get_WhenNoteNotExists_ThenReturnNull()
    {
        Assert.IsNull(await _noteService.Get(Guid.NewGuid()));
    }

    [Test]
    public async Task Get_WhenNoteExists_ThenReturnNote()
    {
        var note = new NoteDto() { CategoryId = Guid.NewGuid(), Content = "test" };
        var savedNote = await _noteService.Create(note);

        var res = await _noteService.Get(savedNote.Id);

        Assert.NotNull(res);
    }

    [Test]
    public async Task Remove_ThenReturnNote()
    {
        var note = new NoteDto() { CategoryId = Guid.NewGuid(), Content = "test" };
        var savedNote = await _noteService.Create(note);

        var res = await _noteService.Remove(savedNote.Id);
        var removedNote = await _noteService.Get(savedNote.Id);

        Assert.NotNull(res);
        Assert.Null(removedNote);
    }

    [Test]
    public async Task Remove_WhenNoteNotExists_ThenThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _noteService.Remove(Guid.NewGuid()));
    }
}