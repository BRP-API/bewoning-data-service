using Brp.AutorisatieEnProtocollering.Proxy.Data;
using Brp.Shared.Infrastructure.Autorisatie;
using Microsoft.FeatureManagement;
using System.Text.RegularExpressions;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie;

public abstract class AbstractAutorisatieService : IAuthorisation
{
    private readonly IServiceProvider _serviceProvider;

    protected AbstractAutorisatieService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public virtual AuthorisationResult Authorize(int afnemerCode, int? gemeenteCode, string requestBody) => throw new NotImplementedException();

    protected Data.Autorisatie? GetActueleAutorisatieFor(int afnemerCode)
    {
        var featureManager = _serviceProvider.GetRequiredService<IFeatureManager>();

        var gebruikMeestRecenteAutorisatie = featureManager.IsEnabledAsync("gebruikMeestRecenteAutorisatie").Result;

        using var scope = _serviceProvider.CreateScope();

        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return gebruikMeestRecenteAutorisatie
            ? appDbContext.Autorisaties
                          .Where(a => a.AfnemerCode == afnemerCode)
                          .OrderByDescending(a => a.TabelRegelStartDatum)
                          .FirstOrDefault()
            : appDbContext.Autorisaties
                          .FirstOrDefault(a => a.AfnemerCode == afnemerCode &&
                                               a.TabelRegelStartDatum <= Vandaag() &&
                                               (a.TabelRegelEindDatum == null || a.TabelRegelEindDatum > Vandaag()));
    }

    private static long Vandaag()
    {
        return int.Parse(DateTime.Today.ToString("yyyyMMdd"));
    }

    protected static IEnumerable<string> BepaalNietGeautoriseerdeElementNamen(IEnumerable<string> geautoriseerdeElementen,
                                                                              IEnumerable<(string Name, string[] Value)> gevraagdeElementen)
    {
        var retval = new List<string>();

        foreach (var (Name, Value) in gevraagdeElementen)
        {
            if (Value.Length == 0 && Name == "ouders.ouderAanduiding" &&
                !IsGeautoriseerdVoorOuderAanduidingVraag(geautoriseerdeElementen))
            {
                retval.Add(Name);
            }
         
            foreach (var gevraagdElementNr in Value)
            {
                if (!geautoriseerdeElementen.Any(x => gevraagdElementNr == x.PrefixWithZero()))
                {
                    retval.Add(Name);
                }
            }
        }

        return retval.Distinct();
    }

    private static bool IsGeautoriseerdVoorOuderAanduidingVraag(IEnumerable<string> geautoriseerdeElementen)
    {
        var ouder1Regex = new Regex(@"^(02(01|02|03|04|62)\d{2}|PAOU01)$");
        var ouder2Regex = new Regex(@"^(03(01|02|03|04|62)\d{2}|PAOU01)$");

        var isGeautoriseerdVoorOuder1 = false;
        var isGeautoriseerdVoorOuder2 = false;

        foreach (var elementNr in geautoriseerdeElementen)
        {
            var prefixedElementNr = elementNr.PrefixWithZero();
            if (ouder1Regex.IsMatch(prefixedElementNr))
            {
                isGeautoriseerdVoorOuder1 = true;
            }
            if (ouder2Regex.IsMatch(prefixedElementNr))
            {
                isGeautoriseerdVoorOuder2 = true;
            }
        }

        return isGeautoriseerdVoorOuder1 && isGeautoriseerdVoorOuder2;
    }

    protected static bool GeenAutorisatieOfNietGeautoriseerdVoorAdHocGegevensverstrekking(Data.Autorisatie? autorisatie)
    {
        return autorisatie == null || !new[] { 'A', 'N' }.Contains(autorisatie.AdHocMedium);
    }

    protected static AuthorisationResult NietGeautoriseerdVoorAdhocGegevensverstrekking(Data.Autorisatie? autorisatie, int afnemerCode) =>
        NotAuthorized(
            title: "U bent niet geautoriseerd voor het gebruik van deze API.",
            detail: "Niet geautoriseerd voor ad hoc bevragingen.",
            code: "unauthorized",
            reason: autorisatie != null
                ? $"Vereiste ad_hoc_medium: A of N. Werkelijk: {autorisatie.AdHocMedium}. Afnemer code: {autorisatie.AfnemerCode}"
                : $"Geen\\Verlopen autorisatie gevonden voor Afnemer code {afnemerCode}");

    protected static AuthorisationResult NietGeautoriseerdVoorEndpoint(string endpoint, IEnumerable<string> nietGeautoriseerdFieldNames, int afnemerCode) =>
        NotAuthorized(title: "U bent niet geautoriseerd voor het gebruik van deze API.",
                      code: "unauthorized",
                      detail: $"Niet geautoriseerd voor {endpoint}.",
                      reason: $"afnemer '{afnemerCode}' is niet geautoriseerd voor {string.Join(", ", nietGeautoriseerdFieldNames.OrderBy(x => x))}");

    protected static AuthorisationResult NietGeautoriseerdVoorFields(IEnumerable<string> nietGeautoriseerdFieldNames, int afnemerCode) =>
        NotAuthorized(title: "U bent niet geautoriseerd voor één of meerdere opgegeven field waarden.",
                      code: "unauthorizedField",
                      reason: $"afnemer '{afnemerCode}' is niet geautoriseerd voor fields {string.Join(", ", nietGeautoriseerdFieldNames.OrderBy(x => x))}");

    protected static AuthorisationResult NietGeautoriseerdVoorParameters(IEnumerable<string> nietGeautoriseerdQueryElementNrs) =>
        NotAuthorized(title: "U bent niet geautoriseerd voor de gebruikte parameter(s).",
                      detail: $"U bent niet geautoriseerd voor het gebruik van parameter(s): {string.Join(", ", nietGeautoriseerdQueryElementNrs.OrderBy(x => x))}.",
                      code: "unauthorizedParameter");

    protected static AuthorisationResult NotAuthorized(string? title = null, string? detail = null, string? code = null, string? reason = null)
    {
        return new AuthorisationResult(
            false,
            new List<AuthorisationFailure>
            {
                new()
                {
                    Title = title,
                    Detail = detail,
                    Code = code,
                    Reason = reason
                }
            }
        );
    }

    protected static AuthorisationResult Authorized()
    {
        return new AuthorisationResult(
            true,
            new List<AuthorisationFailure>()
        );
    }
}

internal static class AutorisationServiceHelpers
{
    public static string PrefixWithZero(this string str)
    {
        return str.Length == 6
            ? str
            : $"0{str}";
    }
}
