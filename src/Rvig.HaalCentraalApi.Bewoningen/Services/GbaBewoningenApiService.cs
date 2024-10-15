using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Bewoningen.Interfaces;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;
using Rvig.HaalCentraalApi.Bewoningen.Fields;
using Microsoft.Extensions.Options;
using Rvig.HaalCentraalApi.Shared.Options;

namespace Rvig.HaalCentraalApi.Bewoningen.Services;
public interface IGbaBewoningenApiService
{
	Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model);
}

public class GbaBewoningenApiService : BaseApiServiceWithProtocolleringAuthorization, IGbaBewoningenApiService
{
	protected IGetAndMapGbaBewoningenService _bewoningenService;
    private readonly IValidatieService _validatieService;
    private readonly IFilterService _filterService;

	protected override BewoningenFieldsSettings _fieldsSettings => new();

    public GbaBewoningenApiService(
        IGetAndMapGbaBewoningenService bewoningenService,
        IValidatieService validatieService,
        IDomeinTabellenRepo domeinTabellenRepo,
        IProtocolleringService protocolleringService,
        ILoggingHelper loggingHelper,
        IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions,
        IFilterService filterService)
        : base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
    {
        _bewoningenService = bewoningenService;
        _validatieService = validatieService;
        _filterService = filterService;
    }

    /// <summary>
    /// Get Bewoning (GBA) 2.0.0 bewoningen via child of BewoningenQuery.
    /// </summary>
    /// <param name="model">Child of BewoningenQuery.</param>
    /// <returns>GbaBewoningenQueryResponse containing bewoningen</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model)
	{
        _validatieService.ValideerModel(model);

        var plIds = new List<long>();
        var useAuthorizationChecks = _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks;
        var useProtocollering = _protocolleringAuthorizationOptions.Value.UseProtocollering;

        (var bewoningen, int afnemerCode) = await _bewoningenService.GetBewoningen(model, useAuthorizationChecks);

        var response = new GbaBewoningenQueryResponse { Bewoningen = bewoningen.ToList() };

        if(useProtocollering)
        {
            plIds = await LogAllBewoningenForProtocollering(response, afnemerCode);
        }

        _filterService.FilterResponse(model, response);

        return (response, plIds);
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
}