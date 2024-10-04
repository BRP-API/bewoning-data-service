using Microsoft.AspNetCore.Mvc;

namespace Brp.Referentie.Api.Controllers;

[ApiController,
Route("haalcentraal/api/reisdocumenten")]
public class ReisdocumentController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public ReisdocumentController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost,
    Route("reisdocumenten")]
    public async Task<IActionResult> Index([FromBody]object body)
    {
        await HttpContext.Response.AddCustomResponseHeaders(_environment);

        if(await HttpContext.Response.AddCustomResponseBody(_environment))
        {
            return Ok();
        }

        return Ok(new { reisdocumenten = new List<object>() });
    }
}
