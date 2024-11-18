using System.ComponentModel;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Category> Categories { get; set; } = null!;
}