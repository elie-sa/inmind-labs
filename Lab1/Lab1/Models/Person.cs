using System.ComponentModel.DataAnnotations;

namespace Lab1.Models;

public class Person
{
    public long Id { get; set; }
    
    // Changed Name to Username to have a different property than "Name" found in the User class
    public string Username { get; set; }
    
    // Changed Email to DifferentNameEmail to demonstrate that I also handled cases where properties have different names
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string DifferentNameEmail { get; set; }
}