using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.ResponseModels.Bewoning;

namespace Bewoning.Api.Interfaces;

public interface IGetAndMapGbaBewoningenService
{
    Task<IEnumerable<GbaBewoning>> GetBewoningen(BewoningenQuery query);

    Task<GbaBewoningenQueryResponse> GetMedebewoners(string burgerservicenummer, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
}
