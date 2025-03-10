using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.ResponseModels.Bewoning;
using Bewoning.Api.Services;
using Bewoning.Api.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Bewoning.Api.Controllers;
[ApiController]
[Route("haalcentraal/api/bewoning")]
[ValidateContentTypeHeader]
public class GbaApiBewoningController : GbaApiBaseController
{
    private readonly IGbaBewoningenApiService _gbaService;

    public GbaApiBewoningController(IGbaBewoningenApiService gbaService)
    {
        _gbaService = gbaService;
    }

    [HttpPost]
    [Route("bewoningen")]
    [ValidateUnusableQueryParams]
    public async Task<GbaBewoningenQueryResponse> GetBewoningen([FromBody] BewoningenQuery model)
    {
        await ValidateUnusableQueryParams(model);

        (GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds) = await _gbaService.GetBewoningen(model);
        AddPlIdsToResponseHeaders(plIds);

        return bewoningenResponse;
    }
}