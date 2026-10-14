
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly UnitConversionService _conversionService;

    public ConversionController(UnitConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpGet("convert")]
    public ActionResult<ConversionResponse> Convert(
        [FromQuery] double value,
        [FromQuery] string from,
        [FromQuery] string to)
    {
        if (!double.IsFinite(value))
        {
            return BadRequest(new
            {
                error = "Value must be a finite number."
            });
        }

        if (string.IsNullOrWhiteSpace(from) ||
            string.IsNullOrWhiteSpace(to))
        {
            return BadRequest(new
            {
                error = "Source and target units are required."
            });
        }

        try
        {
            double result = _conversionService.Convert(value, from, to);

            if (!double.IsFinite(result))
            {
                return BadRequest(new
                {
                    error = "Conversion produced an invalid result."
                });
            }

            return Ok(new ConversionResponse
            {
                OriginalValue = value,
                FromUnit = from,
                ToUnit = to,
                ConvertedValue = Math.Round(result, 4)
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }
}
