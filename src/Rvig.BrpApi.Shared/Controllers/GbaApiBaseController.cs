using Microsoft.AspNetCore.Mvc;
using Rvig.BrpApi.Shared.Validation;

namespace Rvig.BrpApi.Shared.Controllers;

public class GbaApiBaseController : ControllerBase
{
	/// <summary>
	/// This checks if the given model serialized from JSON contains params that are unknown to the requested model type.
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	protected Task ValidateUnusableQueryParams(object model)
	{
		return ApiCallValidator.ValidateUnusableQueryParams(model, HttpContext);
	}

	/// <summary>
	/// The information service (proxy) requires the persoonslijst id (pl-id) of all persons found and returned in a response.
	/// </summary>
	/// <param name="plIds"></param>
	protected void AddPlIdsToResponseHeaders(List<long>? plIds)
	{
		if (plIds?.Any() == true)
		{
			Response.Headers.Add("x-geleverde-pls", string.Join(",", plIds.OrderBy(plId => plId)));
		}
	}
}