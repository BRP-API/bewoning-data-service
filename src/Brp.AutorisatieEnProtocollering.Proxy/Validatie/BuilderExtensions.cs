using Brp.Shared.Infrastructure.Validatie;

namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie;

public static class BuilderExtensions
{
    public static void SetupProtocollering(this WebApplicationBuilder builder)
    {
        builder.Services.AddKeyedTransient<IRequestBodyValidator, Personen.RequestBodyValidatieService>("personen");
        builder.Services.AddKeyedTransient<IRequestBodyValidator, Reisdocumenten.RequestBodyValidatieService>("reisdocumenten");
        builder.Services.AddKeyedTransient<IRequestBodyValidator, Historie.RequestBodyValidatieService>("verblijfplaatshistorie");
    }
}
