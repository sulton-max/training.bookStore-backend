using BookStore.Core.Models.Entities;
using BookStore.DAL.EntityMaps;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Contexts;

/// <summary>
/// Represents General DbContext of Application
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
    }
}
