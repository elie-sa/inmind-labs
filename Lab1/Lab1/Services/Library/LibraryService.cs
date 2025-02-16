using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Services.Library;

public class LibraryService: ILibraryService
{
    private static List<Models.Book> _books = new List<Models.Book>
    {
        new Models.Book{ Id = 1, Title = "The Brothers Karamazov", Ibsn = "3456245127875", AuthorID = 1, PublicationYear = 1880},
        new Models.Book{ Id = 2, Title = "Crime and Punishment", Ibsn = "6324531245623", AuthorID = 1, PublicationYear = 1862},
        new Models.Book{ Id = 3, Title = "Brave New World", Ibsn = "6245551248523", AuthorID =4, PublicationYear = 1932},
        new Models.Book{ Id = 4, Title = "The Metamorphosis", Ibsn = "7452159321456", AuthorID = 2, PublicationYear = 1880},
        new Models.Book{ Id = 5, Title = "Animal Farm", Ibsn = "6475952156321", AuthorID = 5, PublicationYear = 1945},
    };

    private static List<Models.Author> _authors = new List<Models.Author>
    {
        new Models.Author{ Id = 1, Name = "Fyodor Dostoevsky", BirthDate = new DateOnly(1821,11,11), Country = "Russia"},
        new Models.Author{ Id = 2, Name = "Franz Kafka", BirthDate = new DateOnly(1883, 7,3), Country = "Czech Republic"},
        new Models.Author{ Id = 3, Name = "John Steinback", BirthDate = new DateOnly(1902, 2, 27), Country = "United States of America"},
        //original birth date without testing for aldous is 1894, 7, 26 and country United Kingdom
        new Models.Author{ Id = 4, Name = "Aldous Huxley", BirthDate = new DateOnly(1902, 7,14), Country = "United States of America"},
        new Models.Author{ Id = 5, Name = "George Orwell", BirthDate = new DateOnly(1903, 6, 25), Country = "United Kingdom"}
    };
    
    // since the only options to display data is ascending or descending I will provide it as a boolean
    // i made the default be ascending since it is usually optional to have sorting and it will be passed in the parameter 
    public List<Book> GetBooksByYear(int? year, bool? isAscending)
    {
        if (!year.HasValue)
        {
            throw new ArgumentNullException(nameof(year), "Year cannot be null");
        }

        //ascending if the user doesn't provide a value
        if (!isAscending.HasValue)
        {
            isAscending = true;
        }
        
        // I ordered them by title instead of year since they're all in the same year and i implemented the PublishedYear as a single year string since i assumed by the name they didn't need the release date only the year
        // I am using .Value since i have made isAscending nullable (i have given it a default true value it will never throw an error)
        List<Book> result = isAscending!.Value
            ? _books.Where(b => b.PublicationYear == year).OrderBy(b => b.Title).ToList()
            : _books.Where(b => b.PublicationYear == year).OrderByDescending(b => b.Title).ToList();
        
        return result;
    }

    // chose to return List<object> instead of a specific class which included DateOnly and List<Author> attributes since I won't be doing anything with the data in the controller only returning them
    // and once I return them using Ok() i will be turning them into json objects anyway
    public List<object> GetSameYearAuthors()
    {
        return _authors.GroupBy(a => a.BirthDate.Year).Select(b => new
        {
            BirthYear = b.Key, Authors = b.ToList()
        }).ToList<object>();
    }
    
    // Both of these would have duplicates in properties BirthYear (but the full date will be shown in the json object author)
    // And this function would have duplicate in Country with the author properties
    // However I thought the grouping would be clearer if we had the grouping criteria before the group items
    public List<object> GetSameYearCountryAuthors()
    {
        return _authors.GroupBy(a => new {a.BirthDate.Year, a.Country}).Select(b => new
        {
            BirthYear = b.Key.Year,
            Country = b.Key.Country,
            Authors = b.ToList()
        }).ToList<object>();
    }

    public int CalculateNumberOfBooks()
    {
        return _books.Count;
    }

    // returned the TotalPages to let the frontend know how many pages are left/ let him make a correct query
    // the current page is also returned to keep track (and also to calculate whether a request to the next or previous page can be made
    // I could've also included hasNext and hasPrev to indicate whether he can go to the next or prev page or not but didn't because it wasn't asked (client could handle it this way)
    public List<object> GetPaginatedBooks(int? pageNumber, int? pageSize)
    {
        if (!pageNumber.HasValue || pageNumber.Value <= 0)
        {
            throw new ArgumentNullException(nameof(pageNumber), "Page number cannot be null or <= 0");
        }

        if (!pageSize.HasValue)
        {
            throw new ArgumentNullException(nameof(pageSize), "Page size cannot be null");
        }
        
        // I'm casting both since I'm getting them as nullable however i am checking if they have a value first so the casting never lead to an exception
        int totalPages = (int)Math.Ceiling((decimal)((double)_books.Count / pageSize));

        if (pageNumber > totalPages)
        {
            throw new ArgumentException($"Page number {pageNumber} is greater than the maximum number of pages: {totalPages}.");
        } 
            
        List<Book> books = _books.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToList();
        
        return new List<object>()
        {
            new
            {
                Books = books,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            }
        };
    }
}