using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Lab1.Controllers;

[Route("odata/Books")]
public class BookController: ODataController
{
    private readonly LibrarydbContext _context;
    
    public BookController(LibrarydbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IQueryable<Book> Get()
    {
        return _context.Books;
    }
}

// OData Queries

/* Question 1:
Retrieve all books published in a specific year sent by the user, ordered by release date
(ascending or descending based on user choice).

I'm sorting by year PublishedYear having it equal to 1880 and ordering by title ascending in this request
I ordered by title since I couldn't order by releaseDate (PublishedYear is an integer not a date)
http://localhost:5075/odata/Books?$filter=PublishedYear eq 1880 &$orderby=Title asc

Descending Order
http://localhost:5075/odata/Books?$filter=PublishedYear eq 1880 &$orderby=Title desc
*/

/* Question 4:
Calculate the total number of books in the library.

could've added $count=true to get a count with the data but the apply aggregate returned the count only
http://localhost:5075/odata/Books?$apply=aggregate($count as NumberOfBooks)
*/

/* Question 5:
Retrieve a specified number of book records determined by a page size, skipping a specified
number of records based on the current page number

http://localhost:5075/odata/books?$skip={pageNum * pageSize}&$top={pageSize}
we can't calculate the * in OData 
i did them manually and i assume the frontend would do the multiplication
i could've used a regular endpoint for pagination for easier use as i did in Lab3

Example:
http://localhost:5075/odata/Books?$skip=2&$top=2

i wanted to return the total number of pages as i did in lab3 but i couldn't compute the total number of pages on OData i would have to create a normal endpoint
*/