using Core.Models;

namespace Tests.Fakes.Database;

public static class FakeDb
{
    public static List<Category> Categories = new ();
    public static List<Note> Notes = new();

    public static void ClearDb()
    {
        Categories.Clear();
        Notes.Clear();
    }
}