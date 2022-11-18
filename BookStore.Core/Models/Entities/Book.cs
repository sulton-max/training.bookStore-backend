using BookStore.Core.Models.Common;

namespace BookStore.Core.Models.Entities;

/// <summary>
/// Represents entity of physical Book item
/// </summary>
public class Book : IEntity
{
    /// <summary>
    /// Gets or Sets primary Id of entity
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of a book
    /// </summary>
    public string Name { get; set; } = null!;
}