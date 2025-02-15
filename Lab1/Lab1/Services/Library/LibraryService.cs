using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Services.Library;

public class LibraryService: ILibraryService
{
    private static List<Models.Book> _books = new List<Models.Book>
    {
        new Models.Book{ Id = 1, Title = "The Brothers Karamazov", Ibsn = "3456245127875", AuthorID = 1, PublicationYear = 1880},
        new Models.Book{ Id = 2, Title = "Crime and Punishment", Ibsn = "6324531245623", AuthorID = 1, PublicationYear = 1866},
        new Models.Book{ Id = 3, Title = "Brave New World", Ibsn = "6245551248523", AuthorID =4, PublicationYear = 1932},
        new Models.Book{ Id = 4, Title = "The Metamorphosis", Ibsn = "7452159321456", AuthorID = 2, PublicationYear = 1915},
        new Models.Book{ Id = 5, Title = "Animal Farm", Ibsn = "6475952156321", AuthorID = 5, PublicationYear = 1945},
    };

    private static List<Models.Author> _authors = new List<Models.Author>
    {
        new Models.Author{ Id = 1, Name = "Fyodor Dostoevsky", BirthDate = new DateOnly(1821,11,11), Country = "Russia"},
        new Models.Author{ Id = 2, Name = "Franz Kafka", BirthDate = new DateOnly(1883, 7,3), Country = "Czech Republic"},
        new Models.Author{ Id = 3, Name = "John Steinback", BirthDate = new DateOnly(1902, 2, 27), Country = "United States of America"},
        new Models.Author{ Id = 4, Name = "Aldous Huxley", BirthDate = new DateOnly(1894, 7,26), Country = "United Kingdom"},
        new Models.Author{ Id = 5, Name = "George Orwell", BirthDate = new DateOnly(1903, 6, 25), Country = "United Kingdom"}
    };
    
    // since the only options to display data is ascending or descending I will provide it as a boolean
    public List<Book> GetBooksByYear(int? year, bool? isAscending)
    {
        if (!year.HasValue)
        {
            throw new ArgumentNullException(nameof(year), "Year cannot be null");
        }

        if (!isAscending.HasValue)
        {
            throw new ArgumentNullException(nameof(isAscending), "Sorting order cannot be null");
        }
        
        // I ordered them by title instead of year since they're all in the same year and i implemented the PublishedYear as a single year string since i assumed by the name they didn't need the release date only the year
        // I am using .Value since i have made isAscending as nullable
        List<Book> result = (isAscending.Value)
            ? _books.Where(b => b.PublicationYear == year).OrderBy(b => b.Title).ToList()
            : _books.Where(b => b.PublicationYear == year).OrderByDescending(b => b.Title).ToList();
        
        return result;
    }
}