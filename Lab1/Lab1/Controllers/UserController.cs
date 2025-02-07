using Lab1.Models;
using Lab1.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Question 4
    // Question 6 (Merged with question 4 to avoid having redundant APIs)
    // If we don't provide any optional parameter we display all users else we filter according to the search entry
    // and return an empty list if the searchEntry is not found
    [HttpGet]
    [Route("all")]
    public ActionResult<List<User>> Get([FromQuery] string? searchEntry)
    {
        var users = _userService.GetAllUsers(searchEntry);
        return Ok(users);
    }

    // Question 5
    // Input Validated by checking whether the id provided by the client exists in the list
    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(int id)
    {   
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    // Question 8
    // We could also feed the separate id, name, and email and search for the user to change using the id such as in question 5
    // Exception handled: all fields are required, and the id provided must be valid 
    [HttpPost]
    [Route("edit")]
    public IActionResult Edit([FromBody] User user)
    {
        
        _userService.EditUser(user);
        return Ok(new { message = "User updated" });
    }

    // Question 9
    /*
    // This code is the inefficient version and is synchronous which could cause problems when trying to call multiple requests at once
    // The uncommented code below will be synchronous and will include a Task<IActionResult> since I'm dealing with async functions
    [HttpPost]
    [Route("uploadPicture")]
    public void UploadPicture([FromForm] IFormFile file)
    {
        var filePath = Path.Combine("wwwroot", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
    } */

    // If the file is not provided, an error 400 will be thrown
    // [FromForm] worked on postman but caused issues on swagger even when I added [Consumes("multipart/form-data")]
    // Based on my research swagger couldn't use the [FromFile] attribute
    [HttpPost]
    [Route("uploadPicture")]
    public async Task<IActionResult> UploadPicture(IFormFile file)
    {
        await _userService.UploadPicture(file);
        return Ok(new { message = "File uploaded successfully" });
    }

    // Question 10
    // Input validation: if the id == 0 (default), then it is not provided
    // If the id is provided but invalid, an error will also be thrown
    [HttpDelete]
    [Route("delete/{id}")]
    public IActionResult Delete(int id)
    {
        _userService.DeleteUser(id);
        return Ok(new { message = "User deleted successfully" });
    }
}
