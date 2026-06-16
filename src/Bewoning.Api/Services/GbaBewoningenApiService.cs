using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.Interfaces;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.ResponseModels.Bewoning;
using AutoMapper;

namespace Bewoning.Api.Services;
public interface IGbaBewoningenApiService
{
    Task<(GbaBewoningenQueryResponse bewoningenResponse, List<long>? plIds)> GetBewoningen(BewoningenQuery model);
}

public class GbaBewoningenApiService(
    IGetAndMapGbaBewoningenService bewoningenService,
    IFilterService filterService,
    IMapper mapper) : IGbaBewoningenApiService
{
    protected IGetAndMapGbaBewoningenService _bewoningenService = bewoningenService;

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

        filterService.FilterResponse(model, response);

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
        return mapper.Map<Generated.GbaBewoning>(bewoning);
    }
}