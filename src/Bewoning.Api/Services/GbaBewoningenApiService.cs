using Microsoft.Extensions.Options;
using Bewoning.Api.Helpers;
using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.Options;
using Bewoning.Api.Interfaces;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.Fields;
using Bewoning.Api.ResponseModels.Bewoning;
using AutoMapper;

namespace Bewoning.Api.Services;
public interface IGbaBewoningenApiService
{
    Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model);
}

public class GbaBewoningenApiService : BaseApiServiceWithProtocolleringAuthorization, IGbaBewoningenApiService
{
    protected IGetAndMapGbaBewoningenService _bewoningenService;
    private readonly IFilterService _filterService;
    private readonly IMapper _mapper;

    protected override BewoningenFieldsSettings _fieldsSettings => new();

    public GbaBewoningenApiService(
        IGetAndMapGbaBewoningenService bewoningenService,
        IDomeinTabellenRepo domeinTabellenRepo,
        IProtocolleringService protocolleringService,
        ILoggingHelper loggingHelper,
        IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions,
        IFilterService filterService,
        IMapper mapper)
        : base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
    {
        _bewoningenService = bewoningenService;
        _filterService = filterService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get Bewoning (GBA) 2.0.0 bewoningen via child of BewoningenQuery.
    /// </summary>
    /// <param name="model">Child of BewoningenQuery.</param>
    /// <returns>GbaBewoningenQueryResponse containing bewoningen</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model)
    {
        var bewoningen = await _bewoningenService.GetBewoningen(model);

        var response = new GbaBewoningenQueryResponse { Bewoningen = bewoningen.ToList() };

        _filterService.FilterResponse(model, response);

        IEnumerable<(GbaBewoner gbaBewoner, long plId)>? allBewonersForProtocollering = GetAllBewonersForProtocollering(response);
        var plIds = allBewonersForProtocollering?.Select(x => x.plId).Distinct().ToList();

        response.Bewoningen.ConvertAll(MapGbaBewoning);

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

    private Generated.GbaBewoning MapGbaBewoning(GbaBewoning bewoning)
    {
        return _mapper.Map<Generated.GbaBewoning>(bewoning);
    }
}