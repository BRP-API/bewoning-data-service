using Microsoft.AspNetCore.Http;
using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Repositories;
using Rvig.Data.Base.Postgres.Services;
using Rvig.Data.Bewoningen.DatabaseModels;
using Rvig.Data.Bewoningen.Mappers;
using Rvig.Data.Bewoningen.Repositories;
using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.Interfaces;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Util;
using System.Globalization;
using Rvig.Data.Base.Postgres.Helpers;
using Rvig.Data.Base.Services;

namespace Rvig.Data.Bewoningen.Services;
public class GetAndMapGbaBewoningenService : GetAndMapGbaServiceBase, IGetAndMapGbaBewoningenService
{
	private readonly IRvigBewoningenRepo _dbBewoningenRepo;
	private readonly IRvIGDataBewoningenMapper _bewoningenMapper;
	private readonly IDomeinTabellenHelper _domeinTabellenHelper;

	public GetAndMapGbaBewoningenService(IAutorisationRepo autorisationRepo, IRvigBewoningenRepo dbPersoonRepo, IRvIGDataBewoningenMapper bewoningenMapper,
		IHttpContextAccessor httpContextAccessor, IProtocolleringService protocolleringService, IDomeinTabellenHelper domeinTabellenHelper
		)
		: base(httpContextAccessor, autorisationRepo, protocolleringService)
	{
		_dbBewoningenRepo = dbPersoonRepo;
		_bewoningenMapper = bewoningenMapper;
		_domeinTabellenHelper = domeinTabellenHelper;
	}

	/// <summary>
	/// This method is a base class for both GetBewoningen and GetMedebewoners. They both use strings in a data get action but the strings are different so the given func is unique to the operation.
	/// The result however is the same as well as the mapping.
	/// </summary>
	public Task<(IEnumerable<GbaBewoning> bewoningen, int afnemerCode)> GetBewoningen(string adresseerbaarObjectIdentificatie, bool checkAuthorization, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
	{
		return GetMappedBewoningen(adresseerbaarObjectIdentificatie, checkAuthorization, _dbBewoningenRepo.GetBewoningen, _bewoningenMapper.MapBewoning, peildatum, van, tot);
	}

	public async Task<GbaBewoningenQueryResponse> GetMedebewoners(string burgerservicenummer, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
	{
		var mappedObjects = await GetMappedBewoningenWithMedeBewoners(burgerservicenummer, _dbBewoningenRepo.GetMedebewoners, _bewoningenMapper.MapMedebewoner);

		return new GbaBewoningenQueryResponse { Bewoningen = mappedObjects.ToList() };
	}

	/// <summary>
	/// Get mapped bewoning objects value from identification, get bewoning function and map bewoning function
	/// </summary>
	/// <param name="identificatie">This is either a bsn or an adresseerbaarObjectIdentificatie</param>
	/// <param name="getBewoningDataObjectFunc"></param>
	/// <param name="getMappedBewoningObjectFunc"></param>
	/// <returns>Combination of mapped history objects and geheimhoudingpersoonsgegevens value.</returns>
	private async Task<(IEnumerable<GbaBewoning> bewoningen, int afnemerCode)> GetMappedBewoningen(string identificatie, bool checkAuthorization, Func<string, Task<DbBewoningWrapper?>> getBewoningDataObjectFunc, Func<List<(bewoning_bewoner, long)>, List<(bewoning_bewoner, long)>, string?, DateTime?, DateTime?, DateTime?, GbaBewoning?> getMappedBewoningObjectFunc, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
	{
		Afnemer afnemer = new();
		if (checkAuthorization)
		{
			afnemer = GetAfnemer();
		}
		List<GbaBewoning> bewoningen = new();
		var dbBewoningWrapper = await getBewoningDataObjectFunc(identificatie);

		if (dbBewoningWrapper != null && !string.IsNullOrWhiteSpace(dbBewoningWrapper.verblijf_plaats_ident_code))
		{
			List<(bewoning_bewoner dbBewoner, long plId)> allDbBewonersPlIds = dbBewoningWrapper.Bewoners
				.Where(bewonerPlId => bewonerPlId.adres_verblijf_plaats_ident_code?.Equals(identificatie) == true)
				.Select(bewoner => (dbBewoner: bewoner, plId: bewoner.pl_id))
				.ToList();

			if (peildatum.HasValue && !van.HasValue && !tot.HasValue)
			{
				van = peildatum;
				tot = peildatum.Value.AddDays(1);
			}

			var dates = new List<int?>
			{
				int.Parse(van!.Value.ToString("yyyyMMdd")),
				int.Parse(tot!.Value.ToString("yyyyMMdd"))
			};

			// Filter away all previous iterations of the same address because of date period collection. These bewoners will still be necessary for usage for authorisation.
			var allDbBewonersPlIdsFiltered = allDbBewonersPlIds
				.GroupBy(bewonerPlId => bewonerPlId.dbBewoner.pl_id)
				.SelectMany(bewonerPlIdGroup =>
				{
					if (bewonerPlIdGroup.Any(bewonerPlId => bewonerPlId.dbBewoner.vb_volg_nr == 0 && bewonerPlId.dbBewoner.vb_aangifte_adreshouding_oms?.Equals("W") == true))
					{
						return new List<(bewoning_bewoner dbBewoner, long plId)> { bewonerPlIdGroup.OrderBy(bewonerPlId => bewonerPlId.dbBewoner.vb_volg_nr).FirstOrDefault() };
					}

					return bewonerPlIdGroup.OrderBy(bewonerPlId => bewonerPlId.dbBewoner.vb_volg_nr).Select(x => x);
				})
				.ToList();

			allDbBewonersPlIdsFiltered.ForEach(dbBewonerPlId =>
			{
				var currentOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.vb_adreshouding_start_datum?.ToString());
				var nextOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.volgende_start_adres_datum?.ToString());
				var previousOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.vorige_start_adres_datum?.ToString());
				var currentOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(currentOnvolledigeDatum);
				var nextOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(nextOnvolledigeDatum);
				var previousOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(previousOnvolledigeDatum);
				dates.Add(int.Parse(previousOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				dates.Add(int.Parse(previousOnzekerheidsperiode.endOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				dates.Add(int.Parse(currentOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				dates.Add(int.Parse(currentOnzekerheidsperiode.endOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				dates.Add(int.Parse(nextOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				dates.Add(int.Parse(nextOnzekerheidsperiode.endOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				if (dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_reden?.Equals("W") == false && dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_reden?.Equals("F") == false && dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_datum.HasValue)
				{
					var opschortingOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_datum?.ToString());
					var opschortingDatumOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(opschortingOnvolledigeDatum);
					dates.Add(int.Parse(opschortingDatumOnzekerheidsperiode.endOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
					dates.Add(int.Parse(opschortingDatumOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				}
				if ((dbBewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand == 89999 || dbBewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand == 589999))
				{
					var startInOnderzoekOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.vb_onderzoek_start_datum?.ToString());
					var startInOnderzoekDatumOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(startInOnderzoekOnvolledigeDatum);
					dates.Add(int.Parse(startInOnderzoekDatumOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
					var endInOnderzoekOnvolledigeDatum = new DatumOnvolledig(dbBewonerPlId.dbBewoner.vb_onderzoek_eind_datum?.ToString());
					var endInOnderzoekDatumOnzekerheidsperiode = CreateOnzekerheidsPeriodeDateTimes(endInOnderzoekOnvolledigeDatum);
					dates.Add(int.Parse(endInOnderzoekDatumOnzekerheidsperiode.startOnzekerheidsPeriodeDateTime.ToString("yyyyMMdd")));
				}
			});

			dates = dates.Distinct()
				.Where(date => date.HasValue && date >= int.Parse(van.Value.ToString("yyyyMMdd")) && date <= int.Parse(tot.Value.ToString("yyyyMMdd")))
				.OrderBy(date => date)
				.ToList();

			int lastIndex = dates.FindIndex(date => date.Equals(dates[^1]));
			List<(List<(bewoning_bewoner dbBewoner, long plId)> bewonersPlIds, List<(bewoning_bewoner dbBewoner, long plId)> mogelijkeBewonersPlIds, DateTime startDateTime, DateTime? endDateTime)> bewonerFilteringResultsWithPeriods = new();
			foreach (var date in dates)
			{
				var startDateTime = DateTime.ParseExact(date.ToString()!, "yyyyMMdd", CultureInfo.InvariantCulture);
				DateTime? endDateTime = null;
				int index = dates.FindIndex(listItem => listItem.Equals(date));
				if (index != lastIndex)
				{
					endDateTime = DateTime.ParseExact(dates[index + 1]!.Value.ToString()!, "yyyyMMdd", CultureInfo.InvariantCulture);

					allDbBewonersPlIds = RemoveBewonersWithActiveSpecificInOnderzoek(allDbBewonersPlIds, startDateTime, endDateTime);

					// peildatum are null here most likely.
					(List<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, List<(bewoning_bewoner dbBewoner, long plId)> dbMogelijkeBewonersPlIds) = GetBewonersAndMogelijkeBewoners(allDbBewonersPlIds, peildatum, startDateTime, endDateTime);
					if (checkAuthorization)
					{
						await ThrowUnauthorizedExceptionIfNotAuthorized(dbBewonersPlIds.Select(bewonerPlId => bewonerPlId.dbBewoner).Concat(dbMogelijkeBewonersPlIds.Select(bewonerPlId => bewonerPlId.dbBewoner)).Distinct().ToList(), afnemer);
					}
					bewonerFilteringResultsWithPeriods.Add((bewonersPlIds: dbBewonersPlIds, mogelijkeBewonersPlIds: dbMogelijkeBewonersPlIds, startDateTime, endDateTime));
				}
			}
			bewonerFilteringResultsWithPeriods.ForEach(result =>
			{
				var bewoning = getMappedBewoningObjectFunc(result.bewonersPlIds, result.mogelijkeBewonersPlIds, dbBewoningWrapper.verblijf_plaats_ident_code, null, result.startDateTime, result.endDateTime);
				if (bewoning != null)
				{
					bewoningen.Add(bewoning);
				}
			});

            if (bewoningen.Count > 1 && bewoningen.TrueForAll(bewoning => bewoning.IndicatieVeelBewoners == true))
			{
				var bewoning = bewoningen[0];
				bewoning.Periode = new Periode
				{
					DatumVan = van?.ToString("yyyy-MM-dd"),
					DatumTot = tot?.ToString("yyyy-MM-dd")
				};
				bewoningen = new List<GbaBewoning> { bewoning };
			}

			bewoningen = MergeDuplicateBewoningenWithCoincidingPeriods(bewoningen);

			return (bewoningen.Distinct(), afnemer.Afnemerscode);
		}
		return new();
	}

	/// <summary>
	/// There is a use case within Bewoningen where if a bewoner has in onderzoek with number 89999 or 589999 and the in onderzoek period overlaps with the given bewoning period
	/// then the bewoner may not be shown. If the bewoner has an end date to the in onderzoek then they are allowed. It is not tied to showing based on the bewoning period but
	/// rather it is a different process. If one is in onderzoek then you must show the in onderzoek in the bewoner despite the actual bewoning period given. If one isn't in onderzoek or was but it has been dealt with (past, present or future)
	/// then it will not be shown. 89999 and 589999 are exceptions to the rule where you never want to show the bewoner at all.
	/// </summary>
	/// <param name="dbBewoners"></param>
	/// <param name="startDateTime">Bewoning start period</param>
	/// <param name="endDateTime">Bewoning end period</param>
	/// <returns></returns>
	private static List<(bewoning_bewoner dbBewoner, long plId)> RemoveBewonersWithActiveSpecificInOnderzoek(List<(bewoning_bewoner dbBewoner, long plId)> dbBewoners, DateTime startDateTime, DateTime? endDateTime)
	{
		return dbBewoners.Where(dbBewoner =>
		{
			var inOnderzoekBeginDateTime = CreateOnzekerheidsPeriodeDateTimes(new DatumOnvolledig(dbBewoner.dbBewoner.vb_onderzoek_start_datum?.ToString())).startOnzekerheidsPeriodeDateTime;

			return !((dbBewoner.dbBewoner.vb_onderzoek_gegevens_aand == 89999 || dbBewoner.dbBewoner.vb_onderzoek_gegevens_aand == 589999)
				&& !dbBewoner.dbBewoner.vb_onderzoek_eind_datum.HasValue
				&& inOnderzoekBeginDateTime <= startDateTime && DateTime.MaxValue > endDateTime);
		}).ToList();
	}

	private static List<GbaBewoning> MergeDuplicateBewoningenWithCoincidingPeriods(List<GbaBewoning> bewoningen)
	{
		// Sort the list by the start date of the periods in ascending order.
		bewoningen = bewoningen.OrderBy(item => item.Periode?.DatumVan).ToList();

		List<GbaBewoning> mergedList = new();
		GbaBewoning? current = null;

		bewoningen.ForEach(item =>
		{
			if (current == null)
			{
				current = item;
			}
			else if (current.Bewoners != null && item.Bewoners != null && current.Bewoners.SequenceEqual(item.Bewoners)
			&& current.MogelijkeBewoners != null && item.MogelijkeBewoners != null && current.MogelijkeBewoners.SequenceEqual(item.MogelijkeBewoners)
			&& current.Periode?.DatumTot == item.Periode?.DatumVan)
			{
				// Merge the periods by updating the end date.
				if (current.Periode != null)
				{
					current.Periode.DatumTot = item.Periode?.DatumTot;
				}
				else
				{
					current.Periode = new Periode
					{
						DatumTot = item.Periode?.DatumTot
					};
				}
			}
			else
			{
				// Add the current item to the merged list and update the current item.
				mergedList.Add(current);
				current = item;
			}
		});

		// Add the last item to the merged list.
		if (current != null)
		{
			mergedList.Add(current);
		}

		return mergedList;
	}

	private async Task ThrowUnauthorizedExceptionIfNotAuthorized(List<bewoning_bewoner> bewoners, Afnemer afnemer)
	{
		List<(bewoning_bewoner dbBewoner, short? nieuwGemeenteCode)> dbBewonersNewCodes = new();

		foreach (var bewoner in bewoners)
		{
			var nieuweGemeenteCodeDbBewonerPlId = await _domeinTabellenHelper.GetNewGemeenteCode(bewoner.vb_inschrijving_gemeente_code);
			short? nieuweGemeenteCodeAsShort = null;
			if (nieuweGemeenteCodeDbBewonerPlId.HasValue)
			{
				nieuweGemeenteCodeAsShort = (short)nieuweGemeenteCodeDbBewonerPlId;
			}
			dbBewonersNewCodes.Add((dbBewoner: bewoner, nieuwGemeenteCode: nieuweGemeenteCodeAsShort));
		}

		// Rule 1: Een gemeente als afnemer is geautoriseerd voor het vragen van bewoning waarbij voor elke gevonden verblijfplaats op gevraagde adresseerbaar object identificatie en binnen gevraagde periode de gemeente van inschrijving gelijk is aan de gemeentecode in de 'claim' uit de token
		// Rule 2: Een gemeente als afnemer is geautoriseerd voor bewoning, wanneer de gemeente van inschrijving van een gevonden verblijfplaats ongelijk is aan gemeentecode in de token, maar deze gemeente van inschrijving heeft in de gemeententabel nieuwe gemeentecode (92.12) gelijk aan de gemeentecode in de token
		if (!dbBewonersNewCodes.All(bewonerPlId => bewonerPlId.dbBewoner.vb_inschrijving_gemeente_code.Equals(afnemer.Gemeentecode) || bewonerPlId.nieuwGemeenteCode.Equals(afnemer.Gemeentecode)))
		{
			throw new UnauthorizedException("U bent niet geautoriseerd voor deze vraag.", "Je mag alleen bewoning van adresseerbare objecten binnen de eigen gemeente raadplegen.");
		}
	}

	private static (DateTime startOnzekerheidsPeriodeDateTime, DateTime endOnzekerheidsPeriodeDateTime) CreateOnzekerheidsPeriodeDateTimes(DatumOnvolledig current)
	{
		DateTime startOnzekerheidPeriodeDateTime;
		DateTime endOnzekerheidPeriodeDateTime;
		if (!current.IsOnvolledig())
		{
			startOnzekerheidPeriodeDateTime = DateTime.Parse(current.Datum!);
			endOnzekerheidPeriodeDateTime = DateTime.Parse(current.Datum!).AddDays(1);
		}
		else if (current.IsCompleteOnvolledig())
		{
			startOnzekerheidPeriodeDateTime = DateTime.MinValue;
			endOnzekerheidPeriodeDateTime = DateTime.MaxValue;
		}
		else if (!current.Maand.HasValue)
		{
			var startOnzekerheidPeriode = $"{current.Jaar!.Value}-01-01";
			var endOnzekerheidPeriode = $"{current.Jaar!.Value + 1}-01-01";

			startOnzekerheidPeriodeDateTime = DateTime.Parse(startOnzekerheidPeriode);
			endOnzekerheidPeriodeDateTime = DateTime.Parse(endOnzekerheidPeriode);
		}
		else
		{
			var startOnzekerheidPeriode = $"{current.Jaar!.Value}-{current.Maand!.Value}-01";
			var endOnzekerheidPeriode = $"{(current.Maand!.Value == 12 ? current.Jaar!.Value + 1 : current.Jaar!.Value)}-{(current.Maand!.Value == 12 ? 1 : current.Maand!.Value + 1)}-01";

			startOnzekerheidPeriodeDateTime = DateTime.Parse(startOnzekerheidPeriode);
			endOnzekerheidPeriodeDateTime = DateTime.Parse(endOnzekerheidPeriode);
		}

		return (startOnzekerheidPeriodeDateTime, endOnzekerheidPeriodeDateTime);
	}

	private (List<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, List<(bewoning_bewoner dbBewoner, long plId)> dbMogelijkeBewonersPlIds) GetBewonersAndMogelijkeBewoners(List<(bewoning_bewoner dbBewoner, long plId)> bewonersPlIds, DateTime? peildatum, DateTime? van, DateTime? tot)
	{
		foreach (var bewonerPlId in bewonersPlIds)
		{
			if (bewonerPlId.dbBewoner.vb_onderzoek_eind_datum.HasValue && !(bewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand == 89999 || bewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand == 589999))
			{
				bewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand = null;
			}
		}
		List<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds;
		List<(bewoning_bewoner dbBewoner, long plId)> dbMogelijkeBewonersPlIds;
		if (peildatum.HasValue)
		{
			(dbBewonersPlIds, dbMogelijkeBewonersPlIds) = _bewoningenMapper.FilterBewonersByPeildatum(bewonersPlIds, peildatum);
		}
		else if (van.HasValue && tot.HasValue)
		{
			(dbBewonersPlIds, dbMogelijkeBewonersPlIds) = _bewoningenMapper.FilterBewonersByDatumVanDatumTot(bewonersPlIds, van, tot);
		}
		else
		{
			throw new InvalidParamsException("Geen peildatum of datumVan/datumTot geleverd.");
		}

		return (dbBewonersPlIds, dbMogelijkeBewonersPlIds);
	}

	/// <summary>
	/// Get mapped bewoning objects value from identification, get bewoning function and map bewoning function
	/// </summary>
	/// <param name="identificatie">This is either a bsn or an adresseerbaarObjectIdentificatie</param>
	/// <param name="getBewoningDataObjectFunc"></param>
	/// <param name="getMappedBewoningObjectFunc"></param>
	/// <returns>Combination of mapped history objects and geheimhoudingpersoonsgegevens value.</returns>
	private static async Task<IEnumerable<GbaBewoning>> GetMappedBewoningenWithMedeBewoners(string identificatie, Func<string, Task<IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)>>> getBewoningDataObjectFunc,
		Func<IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)>, IEnumerable<GbaBewoning>> getMappedBewoningObjectFunc)
	{
		//(Afnemer afnemer, DbAutorisatie? autorisatie) afnemerAutorisatie = await GetAfnemerAutorisatie(); d

		List<GbaBewoning> historyObjects = new();
		var dbObject = await getBewoningDataObjectFunc(identificatie);
		// AfnemerAutorisatie.Item2 is already validated in GetAfnemerAutorisatie as autorisatie.

		// ToDod
		//var dbFiltered = AuthorisationService.Apply(dbObject, afnemerAutorisatie.autorisatie!, afnemerAutorisatie.afnemer.Gemeentecode); d
		//if (dbFiltered !c= null)d

		if (dbObject != null)
		{
			// Todo2 because if you can have multiple BSN and each BSN can have different geheim_ind, then what is the feature case here?
			//historyObjects = getMappedBewoningObjectFunc(dbFiltered).ToList(); d
			historyObjects = getMappedBewoningObjectFunc(dbObject).ToList();
		}
		// It is impossible to have an empty or null array of bsns because the API request models already validate this and reject all non valid values.
		return historyObjects.Where(x => x != null);
	}
}
