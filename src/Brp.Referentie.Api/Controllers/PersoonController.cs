using Microsoft.AspNetCore.Mvc;
namespace Brp.Referentie.Api.Controllers;

[ApiController,
Route("haalcentraal/api/brp")]
public class PersoonController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public PersoonController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost,
    Route("personen")]
    public async Task<IActionResult> Index([FromBody]object body)
    {
        await HttpContext.Response.AddCustomResponseHeaders(_environment);

        return Ok(new { personen = new List<object>() });
    }
}
