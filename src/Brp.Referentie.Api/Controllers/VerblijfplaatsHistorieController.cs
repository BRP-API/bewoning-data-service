using Microsoft.AspNetCore.Mvc;

namespace Brp.Referentie.Api.Controllers
{
    [ApiController,
    Route("haalcentraal/api/brphistorie")]
    public class VerblijfplaatsHistorieController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public VerblijfplaatsHistorieController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost,
        Route("verblijfplaatshistorie")]
        public async Task<IActionResult> Index([FromBody] object body)
        {
            await HttpContext.Response.AddCustomResponseHeaders(_environment);

            if (await HttpContext.Response.AddCustomResponseBody(_environment))
            {
                return Ok();
            }

            return Ok(new { verblijfplaatsen = new List<object>() });
        }
    }
}
