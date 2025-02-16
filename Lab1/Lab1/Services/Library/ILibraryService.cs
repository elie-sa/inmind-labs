using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Services.Library;

public interface ILibraryService
{
    public List<Book> GetBooksByYear(int? year, bool? isAscending);
    public List<object> GetSameYearAuthors();
    public List<object> GetSameYearCountryAuthors();
    public int CalculateNumberOfBooks();
    public List<object> GetPaginatedBooks(int? pageNumber, int? pageSize);


}