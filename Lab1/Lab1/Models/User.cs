using System.ComponentModel.DataAnnotations;

namespace Lab1.Models;

public class User
{
    [Required(ErrorMessage = "Please enter your Id")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Please enter your Name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please enter your Email")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }
}
