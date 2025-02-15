using System.ComponentModel.DataAnnotations;
namespace Lab1.Models;

public class Book
{
    // I wrote the AuthorID although it is not linked in case we later implemented the same LINQ APIs with the database
    // Naming conventions will also not follow sql but c# instead so the properties will have different names
    
    public required long Id { get; set; }
    
    public required string Title { get; set; }
    
    public long? AuthorID { get; set; }
    
    public string? Ibsn { get; set; }
    
    public int? PublicationYear { get; set; }
}