namespace Lab1.Models;

public class Author
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Country { get; set; }
    
}