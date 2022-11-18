using BookStore.BLL.EntityServices.Interfaces;
using BookStore.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

public class StatsController : CustomControllerBase
{
    private readonly IEntityServiceBase<Book> _bookService;

    public StatsController
    (
        IWebHostEnvironment hostEnvironment, 
        ILogger<StatsController> logger,
        IEntityServiceBase<Book> bookService
    ) : base(hostEnvironment, logger)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Gets total number of books
    /// </summary>
    /// <returns>Number of Books</returns>
    /// <response code="200">Returns Books queried with pagination</response>
    /// <remarks>
    /// Sample request : 
    /// 
    ///     GET stats/
    ///     "Total of 500 books created"
    /// 
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get()
    {
        return Ok($"Total of {(await _bookService.Get(int.MaxValue, 1)).Count()} books created");
    }
}
