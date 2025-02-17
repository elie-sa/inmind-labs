using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Lab1.Controllers;

[Route("odata/Authors")]
public class AuthorController: ODataController
{
    private readonly LibrarydbContext _context;

    public AuthorController(LibrarydbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IQueryable<Author> Get()
    {
        return _context.Authors;
    }
}

/* Question 2:
List groups of authors born in the same year.

OData doesn't support grouping by default so i had a hard time grouping based on year
In addition my BirthDate is a DateOnly data type which doesn't support year(BirthDate) conversion

http://localhost:5075/odata/Authors?$apply=groupby((BirthDate))
I used this for grouping

I tried using the aggregate to add all the properties of the author to the group but it didn't work

I also tried to aggregate the AuthorId alone since the other properties were nullable and that also gave me errors with aggregate
When i tried aggregate(AuthorId with first as Id) without groupby it worked 
http://localhost:5075/odata/Authors?$apply=groupby((BirthDate), aggregate(AuthorId with first as Id))

I will use regular controller functions instead same as the ones in Lab3

*/