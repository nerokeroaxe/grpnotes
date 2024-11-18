using System.ComponentModel;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Database : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
}