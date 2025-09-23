using Bewoning.Api.Helpers;
using Bewoning.Api.Mappers;
using Bewoning.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bewoning.Api;

public static class RegisterServicesExtension
{
    public static void ConfigureRvigApiSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ProtocolleringAuthorizationOptions>(configuration.GetSection(ProtocolleringAuthorizationOptions.ProtocolleringAuthorization));
        services.AddSingleton<ILoggingHelper, LoggingHelper>();
        services.AddAutoMapper(typeof(GbaBewoningMappingProfile).Assembly);
    }
}
