using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rvig.BrpApi.Bewoningen.Helpers;
using Rvig.BrpApi.Bewoningen.Options;

namespace Rvig.BrpApi.Bewoningen;

public static class RegisterServicesExtension
{
    public static void ConfigureRvigApiSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProtocolleringAuthorizationOptions>(configuration.GetSection(ProtocolleringAuthorizationOptions.ProtocolleringAuthorization));
        services.AddSingleton<ILoggingHelper, LoggingHelper>();
    }
}
