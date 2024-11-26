using Rvig.BrpApi.Bewoningen.ApiModels.Bewoning;
using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;
using Rvig.BrpApi.Bewoningen.ResponseModels.Bewoning;

namespace Rvig.BrpApi.Bewoningen.Interfaces;

public interface IGetAndMapGbaBewoningenService
{
	Task<IEnumerable<GbaBewoning>> GetBewoningen(BewoningenQuery query);

	Task<GbaBewoningenQueryResponse> GetMedebewoners(string burgerservicenummer, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
}
