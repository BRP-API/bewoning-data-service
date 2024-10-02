using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Validation;
using Rvig.HaalCentraalApi.Bewoningen.Services;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Shared.Controllers;

namespace Rvig.HaalCentraalApi.Bewoningen.Controllers;
[ApiController]
[Route("haalcentraal/api/bewoning")]
[/*ValidateVersionHeader, */ValidateContentTypeHeader]
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