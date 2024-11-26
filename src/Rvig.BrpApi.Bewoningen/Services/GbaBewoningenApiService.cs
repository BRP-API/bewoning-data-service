using Rvig.BrpApi.Shared.Helpers;
using Rvig.BrpApi.Shared.Interfaces;
using Rvig.BrpApi.Shared.Services;
using Microsoft.Extensions.Options;
using Rvig.BrpApi.Bewoningen.ApiModels.Bewoning;
using Rvig.BrpApi.Bewoningen.Fields;
using Rvig.BrpApi.Bewoningen.Interfaces;
using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;
using Rvig.BrpApi.Bewoningen.ResponseModels.Bewoning;
using Rvig.BrpApi.Shared.Options;

namespace Rvig.BrpApi.Bewoningen.Services;
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

        var bewoningen = await _bewoningenService.GetBewoningen(model);

        var response = new GbaBewoningenQueryResponse { Bewoningen = bewoningen.ToList() };

        _filterService.FilterResponse(model, response);

        IEnumerable<(GbaBewoner gbaBewoner, long plId)>? allBewonersForProtocollering = GetAllBewonersForProtocollering(response);
        plIds = allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList();

        return (response, plIds);
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