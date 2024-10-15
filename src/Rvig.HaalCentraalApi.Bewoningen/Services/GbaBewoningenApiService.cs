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
using Rvig.HaalCentraalApi.Bewoningen.Validation.RequestModelValidators;

namespace Rvig.HaalCentraalApi.Bewoningen.Services;
public interface IGbaBewoningenApiService
{
	Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model);
}

public class GbaBewoningenApiService : BaseApiServiceWithProtocolleringAuthorization, IGbaBewoningenApiService
{
	protected IGetAndMapGbaBewoningenService _bewoningenService;

	protected override BewoningenFieldsSettings _fieldsSettings => new();

	public GbaBewoningenApiService(IGetAndMapGbaBewoningenService getAndMapBewoningenService, IDomeinTabellenRepo domeinTabellenRepo, IProtocolleringService protocolleringService, ILoggingHelper loggingHelper, IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions)
		: base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
	{
        _bewoningenService = getAndMapBewoningenService;
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
			BewoningMetPeildatum bewoningMetPeildatum => await GetBewoningen(nameof(BewoningMetPeildatum), bewoningMetPeildatum),
			BewoningMetPeriode bewoningMetPeriode => await GetBewoningen(nameof(BewoningMetPeriode), bewoningMetPeriode),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
		};
	}

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

    private async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(string modelType, BewoningenQuery model)
    {
        string? adresseerbaarObjectIdentificatie = null;
        DateTime? peildatum = null;
        DateTime? datumVan = null;
        DateTime? datumTot = null;

        switch (modelType)
        {
            case nameof(BewoningMetPeildatum):
                var bewoningMetPeildatum = (BewoningMetPeildatum)model;

                var peildatumValidator = new BewoningMetPeildatumValidator();
                var peildatumValidationResult = peildatumValidator.Validate(bewoningMetPeildatum);

                if (!peildatumValidationResult.IsValid)
                {
                    throw new InvalidParamsException(peildatumValidationResult.Errors.Select(e => new InvalidParams { Code = "validation", Name = e.PropertyName, Reason = e.ErrorMessage }).ToList());
                }

                peildatum = DatumValidator.ValidateAndParseDate(bewoningMetPeildatum.peildatum, "peildatum");
                adresseerbaarObjectIdentificatie = bewoningMetPeildatum.adresseerbaarObjectIdentificatie;
                break;

            case nameof(BewoningMetPeriode):
                var bewoningMetPeriode = (BewoningMetPeriode)model;

                var periodeValidator = new BewoningMetPeriodeValidator();
                var periodeValidationResult = periodeValidator.Validate(bewoningMetPeriode);

                if (!periodeValidationResult.IsValid)
                {
                    throw new InvalidParamsException(periodeValidationResult.Errors.Select(e => new InvalidParams { Code = "validation", Name = e.PropertyName, Reason = e.ErrorMessage }).ToList());
                }

                datumVan = DatumValidator.ValidateAndParseDate(bewoningMetPeriode.datumVan, "datumVan");
                datumTot = DatumValidator.ValidateAndParseDate(bewoningMetPeriode.datumTot, "datumTot");
                adresseerbaarObjectIdentificatie = bewoningMetPeriode.adresseerbaarObjectIdentificatie;
                break;

            default:
                throw new ArgumentException("Onbekend type query");
        }

        (GbaBewoningenQueryResponse bewoningenResponse, int afnemerCode) = await GetGbaBewoningen(adresseerbaarObjectIdentificatie, peildatum, datumVan, datumTot);

        var plIds = new List<long>();

        if (bewoningenResponse?.Bewoningen?.Any() == true)
        {
            if (peildatum.HasValue)
            {
                FilterResponseByPeildatumAndFields(peildatum.Value, bewoningenResponse);
            }
            else if (datumVan.HasValue && datumTot.HasValue)
            {
                FilterResponseByDateAndFields(datumVan.Value, datumTot.Value, bewoningenResponse);
            }

            plIds = await LogAllBewoningenForProtocollering(bewoningenResponse, afnemerCode);
        }

        return (bewoningenResponse ?? new GbaBewoningenQueryResponse { Bewoningen = new List<GbaBewoning>() }, plIds);
    }

    private async Task<(GbaBewoningenQueryResponse, int)> GetGbaBewoningen(string? identificatie, DateTime? peildatum, DateTime? datumVan, DateTime? datumTot)
    {
        return await GetBewoningenMedebewonersBase(
            identificatie,
            _bewoningenService.GetBewoningen,
            _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks,
            peildatum,
            datumVan,
            datumTot
        );
    }

    private async Task<List<long>?> LogAllBewoningenForProtocollering(GbaBewoningenQueryResponse bewoningenResponse, int afnemerCode)
    {
        List<long>? plIds;
        IEnumerable<(GbaBewoner gbaBewoner, long plId)>? allBewonersForProtocollering = GetAllBewonersForProtocollering(bewoningenResponse);
        plIds = allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList();

        if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
        {
            await LogProtocolleringInDb(afnemerCode,
                allBewonersForProtocollering?
                .Select(x => x.plId)
                .Distinct()
                .ToList(),
                new List<string> { "081030", "081180", "081320" },
                new List<string> { "010120" });
        }

        return plIds;
    }

    private static IEnumerable<(GbaBewoner gbaBewoner, long plId)>? GetAllBewonersForProtocollering(GbaBewoningenQueryResponse bewoningenResponse)
    {
        return bewoningenResponse.Bewoningen?.Where(bewoning => bewoning?.BewonersPlIds?
            .Any() == true)
            .SelectMany(bewoning => bewoning.BewonersPlIds!)
            .Concat(bewoningenResponse.Bewoningen
            .Where(bewoning => bewoning?.MogelijkeBewonersPlIds?.Any() == true)
            .SelectMany(bewoning => bewoning.MogelijkeBewonersPlIds!));
    }

    private static void FilterResponseByDateAndFields(DateTime datumVanDateTime, DateTime datumTotDateTime, GbaBewoningenQueryResponse bewoningenResponse)
	{
		bewoningenResponse.Bewoningen = FilterByDatumVanDatumTot(datumVanDateTime,
						datumTotDateTime,
						bewoningenResponse.Bewoningen!,
						$"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}",
						$"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
	}

	private static void FilterResponseByPeildatumAndFields(DateTime peildatumDateTime, GbaBewoningenQueryResponse bewoningenResponse)
	{
        bewoningenResponse.Bewoningen = FilterByPeildatumAndFields(peildatumDateTime, 
			bewoningenResponse.Bewoningen!, 
			$"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}", 
			$"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
    }
}