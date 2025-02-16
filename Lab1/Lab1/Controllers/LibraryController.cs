using Lab1.Models;
using Lab1.Services.Library;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

[ApiController]
[Route("library")]
public class LibraryController: ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }
    
    [HttpGet]
    [Route("books/{year}")]
    public IActionResult GetBooksByYear(int? year, [FromQuery] bool? isAscending)
    {
        var books = _libraryService.GetBooksByYear(year, isAscending);
        return Ok(books);
    }

    [HttpGet]
    [Route("authors/sameYear")]
    public IActionResult GetSameYearAuthors()
    {
        return Ok(_libraryService.GetSameYearAuthors());
    }
    
    [HttpGet]
    [Route("authors/sameYearCountry")]
    public IActionResult GetSameYearCountryAuthors()
    {
        return Ok(_libraryService.GetSameYearCountryAuthors());
    }

    [HttpGet]
    [Route("authors/bookCount")]
    public IActionResult GetBookCount()
    {
        return Ok(new {numOfBooks = _libraryService.CalculateNumberOfBooks()});
    }
    
    // I will be providing the number of books per page and the size of the page in the url
    // error handled in case invalid page number is provided (there isn't any books left to display in next page)
    [HttpGet]
    [Route("books/{pageNumber}/{pageSize}")]
    public IActionResult GetBooks(int? pageNumber, int? pageSize)
    {
        return Ok(_libraryService.GetPaginatedBooks(pageNumber, pageSize));
    }
}