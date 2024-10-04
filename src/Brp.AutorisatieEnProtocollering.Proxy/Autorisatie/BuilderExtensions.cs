using Brp.Shared.Infrastructure.Autorisatie;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie;

public static class BuilderExtensions
{
    public static void SetupAuthorisation(this WebApplicationBuilder builder)
    {
        builder.Services.AddKeyedTransient<IAuthorisation, Personen.AuthorisatieService>("personen");
        builder.Services.AddKeyedTransient<IAuthorisation, Reisdocumenten.AutorisatieService>("reisdocumenten");
        builder.Services.AddKeyedTransient<IAuthorisation, Historie.AutorisatieService>("verblijfplaatshistorie");
    }
}
