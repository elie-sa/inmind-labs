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
    [Route("books")]
    public IActionResult GetBooksByYear([FromQuery] int year, bool isAscending)
    {
        var books = _libraryService.GetBooksByYear(year, isAscending);
        return Ok(books);
    }
}