using BookStore.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DAL.EntityMaps;

/// <summary>
/// Provides configuration for <see cref="Book"/>
/// </summary>
internal class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Name).IsRequired();
    }
}
