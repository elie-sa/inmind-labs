using System.ComponentModel.DataAnnotations;

namespace Lab1.Models;

public class User
{
    
    public required long Id { get; set; }
    
    public required string Name { get; set; }
    
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public required string Email { get; set; }
}
