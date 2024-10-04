using Brp.AutorisatieEnProtocollering.Proxy.Helpers;
using Brp.Shared.Infrastructure.Autorisatie;
using Brp.Shared.Infrastructure.Logging;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Historie;

public class AutorisatieService : AbstractAutorisatieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutorisatieService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        : base(serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override AuthorisationResult Authorize(int afnemerCode, int? gemeenteCode, string requestBody)
    {
        var autorisatieLog = _httpContextAccessor.HttpContext?.GetAutorisatieLog();

        if (gemeenteCode.HasValue)
        {
            if(autorisatieLog != null)
            {
                autorisatieLog.Gemeente = $"afnemer: {afnemerCode} is gemeente '{gemeenteCode}'";
            }
            return Authorized();
        }

        var autorisatie = GetActueleAutorisatieFor(afnemerCode);
        if (autorisatie != null && autorisatieLog != null)
        {
            autorisatieLog.Regel = autorisatie;
        }

        if (GeenAutorisatieOfNietGeautoriseerdVoorAdHocGegevensverstrekking(autorisatie))
        {
            return NietGeautoriseerdVoorAdhocGegevensverstrekking(autorisatie, afnemerCode);
        }

        var geautoriseerdeElementNrs = autorisatie!.RubrieknummerAdHoc!.Split(' ');

        var fieldElementNrs = new[] { "verblijfplaatshistorie" }.ToKeyStringArray(Constanten.FieldElementNrDictionary, "", BepaalKeyVoor);

        var nietGeautoriseerdFieldNames = BepaalNietGeautoriseerdeElementNamen(geautoriseerdeElementNrs, fieldElementNrs);
        if (nietGeautoriseerdFieldNames.Any())
        {
            return NietGeautoriseerdVoorEndpoint("verblijfplaatshistorie", nietGeautoriseerdFieldNames, afnemerCode);
        }

        return Authorized();
    }

    private string BepaalKeyVoor(string field, string zoekType) => field;
}
