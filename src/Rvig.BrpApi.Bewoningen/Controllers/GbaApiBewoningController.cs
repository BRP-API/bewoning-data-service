using Microsoft.AspNetCore.Mvc;
using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;
using Rvig.BrpApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.BrpApi.Bewoningen.Services;
using Rvig.BrpApi.Shared.Validation;
using Rvig.BrpApi.Shared.Controllers;

namespace Rvig.BrpApi.Bewoningen.Controllers;
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