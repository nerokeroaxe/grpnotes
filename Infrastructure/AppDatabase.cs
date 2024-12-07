using System.ComponentModel;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
}