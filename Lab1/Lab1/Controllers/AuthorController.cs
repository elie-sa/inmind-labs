using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Lab1.Controllers;

public class AuthorController: ODataController
{
    private readonly LibrarydbContext _context;

    public AuthorController(LibrarydbContext context)
    {
        _context = context;
    }
    
    [Route("odata/Authors")]
    [EnableQuery]
    public IQueryable<Author> Get()
    {
        return _context.Authors;
    }
    
    // had to use HasValue since the attributes are nullable
    [Route("authors/sameYear")]
    [HttpGet]
    public IActionResult GetSameYearAuthors()
    {
        var authors = _context.Authors
            .Where(a => a.BirthDate.HasValue)
            .GroupBy(a => a.BirthDate.Value.Year) 
            .Select(b => new
            {
                BirthYear = b.Key,
                Authors = b.ToList()
            }).ToList();

        return Ok(authors);
    }
    
    [Route("authors/sameYearCountry")]
    [HttpGet]
    public IActionResult GetSameYearCountryAuthors()
    {
        var authors = _context.Authors
            .Where(a => a.BirthDate.HasValue) 
            .GroupBy(a => new { a.BirthDate.Value.Year, a.Country })
            .Select(b => new
            {
                BirthYear = b.Key.Year,
                Country = b.Key.Country,
                Authors = b.ToList()
            }).ToList();

        return Ok(authors);
    }
}

/* Question 2:
List groups of authors born in the same year.

OData doesn't support grouping by default so i had a hard time grouping based on year
In addition my BirthDate is a DateOnly data type which doesn't support year(BirthDate) conversion

http://localhost:5075/odata/Authors?$apply=groupby((BirthDate))
I used this for grouping

I tried using the aggregate to add all the properties of the author to the group but it didn't work

so I used regular controller functions instead same as the ones in Lab3
*/