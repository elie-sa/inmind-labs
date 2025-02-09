using Lab1.Models;
using Lab1.Services.ObjectMapper;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

[ApiController]
[Route("")]
public class ObjectMappingController: ControllerBase
{
    private readonly IObjectMapperService _objectMapperService;

    public ObjectMappingController(IObjectMapperService objectMapperService)
    {
        _objectMapperService = objectMapperService;
    }

    [HttpPost]
    [Route("mapUser")]
    public IActionResult MapUser([FromBody] User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        Person person = _objectMapperService.Map<User, Person>(user);
        return Ok(person);
    }
}