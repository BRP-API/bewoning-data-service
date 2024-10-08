using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Bewoningen.Interfaces;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;
using Rvig.HaalCentraalApi.Bewoningen.Fields;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Microsoft.Extensions.Options;
using Rvig.HaalCentraalApi.Shared.Options;

namespace Rvig.HaalCentraalApi.Bewoningen.Services;
public interface IGbaBewoningenApiService
{
	Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model);
}

public class GbaBewoningenApiService : BaseApiServiceWithProtocolleringAuthorization, IGbaBewoningenApiService
{
	protected IGetAndMapGbaBewoningenService _getAndMapBewoningenService;

	protected override BewoningenFieldsSettings _fieldsSettings => new();

	public GbaBewoningenApiService(IGetAndMapGbaBewoningenService getAndMapBewoningenService, IDomeinTabellenRepo domeinTabellenRepo, IProtocolleringService protocolleringService, ILoggingHelper loggingHelper, IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions)
		: base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
	{
		_getAndMapBewoningenService = getAndMapBewoningenService;
	}

	/// <summary>
	/// Get Bewoning (GBA) 2.0.0 bewoningen via child of BewoningenQuery.
	/// </summary>
	/// <param name="model">Child of BewoningenQuery.</param>
	/// <returns>GbaBewoningenQueryResponse containing bewoningen</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model)
	{
		return model switch
		{
			//MedebewonersMetPeildatum medebewonersMetPeildatum => await GetMedebewoners(medebewonersMetPeildatum),
			//MedebewonersMetPeriode medebewonersMetPeriode => await GetMedebewoners(medebewonersMetPeriode),
			BewoningMetPeildatum bewoningMetPeildatum => await GetBewoningen(bewoningMetPeildatum),
			BewoningMetPeriode bewoningMetPeriode => await GetBewoningen(bewoningMetPeriode),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
		};
	}

	//private async Task<GbaBewoningenQueryResponse> GetMedebewoners(MedebewonersMetPeildatum model)
	//{
	//	// Validation
	//	if (string.IsNullOrWhiteSpace(model.peildatum))
	//	{
	//		return new GbaBewoningenQueryResponse();
	//	}
	//	var peildatumDateTime = DateTime.Parse(model.peildatum);

	//	// Get bewoningen
	//	var bewoningenMedebewonersResponse = await GetBewoningenMedebewonersBase(model.burgerservicenummer, _getAndMapBewoningenService.GetMedebewoners);

	//	// bewoningenResponse.Bewoningen has already been validated in the GetBewoningenBase method.
	//	// Set periode based on model data.
	//	if (bewoningenMedebewonersResponse.Bewoningen?.Any(bewoning => !ObjectHelper.AllPropertiesDefault(bewoning)) == true)
	//	{
	//		foreach (GbaBewoning bewoning in bewoningenMedebewonersResponse.Bewoningen)
	//		{
	//			bewoning.Periode = new Periode
	//			{
	//				DatumVan = model.peildatum,
	//				DatumTot = peildatumDateTime.AddDays(1).ToString("yyyy-MM-dd")
	//			};
	//		}
	//	}

	//	// Filter response by date and fields
	//	bewoningenMedebewonersResponse.Bewoningen = FilterByPeildatumAndFields(peildatumDateTime, bewoningenMedebewonersResponse.Bewoningen!, $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}", $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
	//	return bewoningenMedebewonersResponse;
	//}

	//private async Task<GbaBewoningenQueryResponse> GetMedebewoners(MedebewonersMetPeriode model)
	//{
	//	// Validation
	//	if (string.IsNullOrWhiteSpace(model.datumVan) || string.IsNullOrWhiteSpace(model.datumTot))
	//	{
	//		return new GbaBewoningenQueryResponse();
	//	}

	//	// Get bewoningen
	//	var bewoningenMedebewonersResponse = await GetBewoningenMedebewonersBase(model.burgerservicenummer, _getAndMapBewoningenService.GetMedebewoners);

	//	// bewoningenResponse.Bewoningen has already been validated in the GetBewoningenBase method.
	//	// Set periode based on model data.
	//	if (bewoningenMedebewonersResponse.Bewoningen?.Any(bewoning => !ObjectHelper.AllPropertiesDefault(bewoning)) == true)
	//	{
	//		foreach (GbaBewoning bewoning in bewoningenMedebewonersResponse.Bewoningen)
	//		{
	//			bewoning.Periode = new Periode
	//			{
	//				DatumVan = model.datumVan,
	//				DatumTot = model.datumTot
	//			};
	//		}
	//	}

	//	// Filter response by dates and fields
	//	bewoningenMedebewonersResponse.Bewoningen = FilterByDatumVanDatumTot(DateTime.Parse(model.datumVan), DateTime.Parse(model.datumTot), bewoningenMedebewonersResponse.Bewoningen!, $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}", $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
	//	return bewoningenMedebewonersResponse;
	//}

	/// <summary>
	/// Get bewoning or medebewoner data based on identificatie. Uses fields and fieldsModel to validate request scope.
	/// Lastly uses getBewoningenFunc as given method unique for each identificatie type to retrieve the data.
	/// </summary>
	/// <param name="identificatie"></param>
	/// <param name="getBewoningenFunc"></param>
	/// <returns></returns>
	private static async Task<(GbaBewoningenQueryResponse bewoningenResponse, int afnemerCode)> GetBewoningenMedebewonersBase(string? identificatie, Func<string, bool, DateTime?, DateTime?, DateTime?, Task<(IEnumerable<GbaBewoning> bewoningen, int afnemerCode)>> getBewoningenFunc, bool checkAuthorization, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
	{
		// Validation
		if (string.IsNullOrEmpty(identificatie))
		{
			return default;
		}

		// Get bewoningen
		(IEnumerable<GbaBewoning> bewoningen, int afnemerCode) = await getBewoningenFunc(identificatie, checkAuthorization, peildatum, van, tot);

		if (bewoningen?.Any() != true)
		{
			return default;
		}

		var bewoningenResponse = new GbaBewoningenQueryResponse { Bewoningen = bewoningen.ToList() };

		// bewoningenResponse.Bewoningen has already been validated in the GetBewoningenBase method.
		// Set periode based on model data.
		if (bewoningenResponse.Bewoningen?.Any(bewoning => !ObjectHelper.AllPropertiesDefault(bewoning)) == true)
		{
			bewoningenResponse.Bewoningen.ForEach(bewoning =>
			{
				bewoning.Periode ??= new Periode
				{
					DatumVan = (van ?? peildatum)?.ToString("yyyy-MM-dd"),
					DatumTot = tot == null
								? peildatum?.AddDays(1).ToString("yyyy-MM-dd")
								: tot?.ToString("yyyy-MM-dd")
				};
			});
		}

		return (bewoningenResponse, afnemerCode);
	}

	private async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningMetPeildatum model)
	{
		// Validation
		DateTime peildatumDateTime;
		if (string.IsNullOrWhiteSpace(model.peildatum))
		{
			return (new GbaBewoningenQueryResponse(), new List<long>());
		}
		else if (!DateTime.TryParse(model.peildatum, out peildatumDateTime))
		{
			throw new InvalidParamsException(new List<InvalidParams> { new() { Code = "date", Name = "peildatum", Reason = "Waarde is geen geldige datum." } });
		}

		// Get bewoningen
		(GbaBewoningenQueryResponse bewoningenResponse, int afnemerCode) = await GetBewoningenMedebewonersBase(model.adresseerbaarObjectIdentificatie, _getAndMapBewoningenService.GetBewoningen, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks, peildatumDateTime);

		var plIds = new List<long>();
		if (bewoningenResponse?.Bewoningen?.Any() == true)
		{
			// Filter response by date and fields
			bewoningenResponse.Bewoningen = FilterByPeildatumAndFields(peildatumDateTime, bewoningenResponse.Bewoningen!, $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}", $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");

			var allBewonersForProtocollering = bewoningenResponse.Bewoningen?
						.Where(bewoning => bewoning?.BewonersPlIds?.Any() == true)
						.SelectMany(bewoning => bewoning.BewonersPlIds!)
				.Concat(bewoningenResponse.Bewoningen
							.Where(bewoning => bewoning?.MogelijkeBewonersPlIds?.Any() == true)
							.SelectMany(bewoning => bewoning.MogelijkeBewonersPlIds!));

			plIds = allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList();

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, plIds,
									new List<string> { "081030", "081180", "081320" },
									new List<string> { "010120" }
								);
			}
		}

		return (bewoningenResponse ?? new GbaBewoningenQueryResponse { Bewoningen = new List<GbaBewoning>() }, plIds);
	}

	private async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningMetPeriode model)
	{
		// Validation
		DateTime datumVanDateTime;
		DateTime datumTotDateTime;
		if (string.IsNullOrWhiteSpace(model.datumVan) || string.IsNullOrWhiteSpace(model.datumTot))
		{
			return (new GbaBewoningenQueryResponse(), new List<long>());
		}
		else if (!DateTime.TryParse(model.datumVan, out datumVanDateTime))
		{
			throw new InvalidParamsException(new List<InvalidParams> { new() { Code = "date", Name = "datumVan", Reason = "Waarde is geen geldige datum." } });
		}
		else if (!DateTime.TryParse(model.datumTot, out datumTotDateTime))
		{
			throw new InvalidParamsException(new List<InvalidParams> { new() { Code = "date", Name = "datumTot", Reason = "Waarde is geen geldige datum." } });
		}
		else if (datumVanDateTime >= datumTotDateTime)
		{
			throw new InvalidParamsException(new List<InvalidParams> { new() { Code = "date", Name = "datumTot", Reason = "datumTot moet na datumVan liggen." } });
		}

		// Get bewoningen
		(GbaBewoningenQueryResponse bewoningenResponse, int afnemerCode) = await GetBewoningenMedebewonersBase(model.adresseerbaarObjectIdentificatie, _getAndMapBewoningenService.GetBewoningen, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks, null, datumVanDateTime, datumTotDateTime);

		var plIds = new List<long>();

		if (bewoningenResponse?.Bewoningen?.Any() == true)
		{
			// Filter response by date and fields
			bewoningenResponse.Bewoningen = FilterByDatumVanDatumTot(datumVanDateTime, datumTotDateTime, bewoningenResponse.Bewoningen!, $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}", $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");

			var allBewonersForProtocollering = bewoningenResponse.Bewoningen?
						.Where(bewoning => bewoning?.BewonersPlIds?.Any() == true)
						.SelectMany(bewoning => bewoning.BewonersPlIds!)
				.Concat(bewoningenResponse.Bewoningen
							.Where(bewoning => bewoning?.MogelijkeBewonersPlIds?.Any() == true)
							.SelectMany(bewoning => bewoning.MogelijkeBewonersPlIds!));

			plIds = allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList();

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList(),
										new List<string> { "081030", "081180", "081320" },
										new List<string> { "010120" }
									);
			}
		}

		return (bewoningenResponse ?? new GbaBewoningenQueryResponse { Bewoningen = new List<GbaBewoning>() }, plIds);
	}
}