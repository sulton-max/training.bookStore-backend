using BookStore.BLL.EntityServices.Interfaces;
using BookStore.Core.Models.Entities;
using BookStore.DAL.Repositories.Interfaces;

namespace BookStore.BLL.EntityServices;

/// <summary>
/// Provide methods for Book item Business Logic
/// </summary>
public class BookService : EntityServiceBase<Book, IRepositoryBase<Book>>, IBookService
{
    public BookService(IRepositoryBase<Book> entityRepository) : base(entityRepository)
    {
    }
}
