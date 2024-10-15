using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;

namespace Rvig.HaalCentraalApi.Bewoningen.Interfaces;

public interface IGetAndMapGbaBewoningenService
{
	Task<(IEnumerable<GbaBewoning> bewoningen, int afnemerCode)> GetBewoningen(BewoningenQuery query, bool checkAuthorization);

	Task<GbaBewoningenQueryResponse> GetMedebewoners(string burgerservicenummer, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
}
