using Brp.Shared.Infrastructure.Protocollering;

namespace Brp.AutorisatieEnProtocollering.Proxy.Protocollering;

public static class BuilderExtensions
{
    public static void SetupRequestValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddKeyedTransient<IProtocollering, Personen.ProtocolleringService>("personen");
        builder.Services.AddKeyedTransient<IProtocollering, Reisdocumenten.ProtocolleringService>("reisdocumenten");
        builder.Services.AddKeyedTransient<IProtocollering, Historie.ProtocolleringService>("verblijfplaatshistorie");
    }
}
