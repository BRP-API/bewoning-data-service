using Brp.AutorisatieEnProtocollering.Proxy.Helpers;
using Brp.Shared.Infrastructure.Protocollering;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Brp.AutorisatieEnProtocollering.Proxy.Protocollering;

public abstract class AbstractProtocolleringService : IProtocollering
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ReadOnlyDictionary<string, string> _fieldElementNrDictionary;

    protected AbstractProtocolleringService(IServiceProvider serviceProvider, ReadOnlyDictionary<string, string> fieldElementNrDictionary)
    {
        _serviceProvider = serviceProvider;
        _fieldElementNrDictionary = fieldElementNrDictionary;
    }

    public bool Protocolleer(int afnemerCode, string geleverdePersoonslijstIds, string requestBody)
    {
        using var scope = _serviceProvider.CreateScope();

        var appDbContext = scope.ServiceProvider.GetRequiredService<Data.AppDbContext>();

        var input = JObject.Parse(requestBody);

        var zoekElementNrs = input.BepaalElementNrVanZoekParameters(_fieldElementNrDictionary);
        var fieldElementNrs = BepaalElementNrVanFieldsVoorProtocollering(input);

        var zoekRubrieken = new List<string>();
        foreach (var (Name, Value) in zoekElementNrs)
        {
            zoekRubrieken.AddRange(Value);
        }
        var requestZoekRubrieken = string.Join(", ", zoekRubrieken.Distinct().OrderBy(x => x));

        var gevraagdeRubrieken = new List<string>();
        foreach (var (Name, Value) in fieldElementNrs)
        {
            gevraagdeRubrieken.AddRange(Value);
        }
        var requestGevraagdeRubrieken = string.Join(", ", gevraagdeRubrieken.Distinct().OrderBy(x => x));

        foreach (var plId in geleverdePersoonslijstIds.Split(','))
        {
            Data.Protocollering protocollering = new()
            {
                RequestId = Guid.NewGuid().ToString(),
                AfnemerCode = afnemerCode,
                PersoonslijstId = long.Parse(plId),
                RequestZoekRubrieken = requestZoekRubrieken,
                RequestGevraagdeRubrieken = requestGevraagdeRubrieken
            };

            appDbContext.Add(protocollering);
        }
        appDbContext.SaveChanges();

        return true;
    }

    protected virtual IEnumerable<(string Name, string[] Value)> BepaalElementNrVanFieldsVoorProtocollering(JObject input)
    {
        throw new NotImplementedException();
    }
}
