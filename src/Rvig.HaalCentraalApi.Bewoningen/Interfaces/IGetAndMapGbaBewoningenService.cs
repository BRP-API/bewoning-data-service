using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning;

namespace Rvig.HaalCentraalApi.Bewoningen.Interfaces;

public interface IGetAndMapGbaBewoningenService
{
	Task<(IEnumerable<GbaBewoning> bewoningen, int afnemerCode)> GetBewoningen(string adresseerbaarObjectIdentificatie, bool checkAuthorization, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
	Task<GbaBewoningenQueryResponse> GetMedebewoners(string burgerservicenummer, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
}
