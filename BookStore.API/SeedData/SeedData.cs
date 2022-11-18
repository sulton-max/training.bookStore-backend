using BookStore.Core.Models.Entities;
using BookStore.DAL.Contexts;

namespace BookStore.API.SeedData;

/// <summary>
/// Provides method to use seed data in Database
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Initializes seed data
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void InitializeSeedData(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext == null)
            throw new InvalidOperationException();

        if (dbContext.Books.Any())
            return;

        dbContext.Books.AddRange(new[]
        {
            new Book
            {
                Name = "API Design patterns"
            },
            new Book
            {
                Name = "Algorithms to Live by"
            },
              new Book
            {
                Name = "Mastermind"
            },
            new Book
            {
                Name = "Ask Powerful Questions"
            },
              new Book
            {
                Name = "Head First Design Patterns"
            },
            new Book
            {
                Name = "Cracking the Coding Interview"
            }
        });

        dbContext.SaveChanges();
    }
}
