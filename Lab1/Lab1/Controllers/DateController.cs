using Microsoft.AspNetCore.Mvc;
using Lab1.Services;
using Lab1.Services.Date;

namespace Lab1.Controllers;

[ApiController]
[Route("")]
public class DateController : ControllerBase
{
    private readonly IDateService _dateService;

    public DateController(IDateService dateService)
    {
        _dateService = dateService;
    }

    [HttpGet]
    [Route("date")]
    public IActionResult GetDate()
    {
        try
        {
            var language = Request.Headers["Accept-Language"].ToString();
            var formattedDate = _dateService.GetFormattedDate(language);
            return Ok(formattedDate);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
