using Contracts.DTOs;
using Contracts.Repositories;
using Contracts.Services;
using Core.Services;
using Tests.Fakes.Database;
using Tests.Fakes.Repositories;

namespace Tests.CategoryTests;

internal class CategoryServiceTests
{
    private ICategoryService _categoryService;
    [SetUp]
    public void Setup()
    {
        _categoryService = new CategoryService(new FakeCategoryRepo());
    }

    [TearDown]
    public void TearDown()
    {
        FakeDb.ClearDb();
    }

    [Test]
    [TestCase("test")]
    [TestCase("1`zqwec0--3259mvcx%#@&*()_+")]
    public async Task Create_ThenSaveAndReturnCategory(string name)
    {
        var category = new CategoryDto() { Name = name };

        var res = await _categoryService.Create(category);
        var fromRepo = await _categoryService.Get(res.Id);

        Assert.NotNull(res);
        Assert.NotNull(res.Id);
        Assert.NotNull(fromRepo);
        Assert.AreEqual(fromRepo.Name, name);
    }

    [Test]
    [TestCase("   ")]
    [TestCase("")]
    public void Create_WhenGivenWrongName_ThenThrowArgumentException(string name)
    {
        var category = new CategoryDto() { Name = name };

        Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Create(category));
    }

    [Test]
    public async Task Create_WhenCategoryAlreadyExists_ThenThrowArgumentException()
    {
        var category = new CategoryDto() { Name = "test" };

        await _categoryService.Create(category);

        Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Create(category));
    }

    [Test]
    public async Task GetAll_ThenReturnAllCategories()
    {
        var category1 = new CategoryDto() { Name = "test1" };
        var category2 = new CategoryDto() { Name = "test2" };
        await _categoryService.Create(category1);
        await _categoryService.Create(category2);

        var categories = await _categoryService.GetAllWithNotes();

        Assert.AreEqual(categories.Count(), 2);

    }

    [Test]
    public async Task GetAll_WhenNoCategories_ThenReturnEmptyList()
    {
        var categories = await _categoryService.GetAllWithNotes();

        Assert.AreEqual(categories.Count(), 0);
    }

    [Test]
    public async Task Remove_ThenRemoveCategory()
    {
        var category = new CategoryDto() { Name = "test" };
        var savedCategory = await _categoryService.Create(category);

        var result = await _categoryService.Remove(savedCategory.Id);
        var removedCategory = await _categoryService.Get(savedCategory.Id);

        Assert.AreEqual(result.Id, savedCategory.Id);
        Assert.IsNull(removedCategory);
    }

    [Test]
    public async Task Remove_WhenGivenWrongId_ThenThrowArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Remove(Guid.NewGuid()));
        Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Remove(Guid.Empty));
    }
}