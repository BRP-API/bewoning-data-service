using Bewoning.Api.Interfaces;
using Bewoning.Api.Options;
using Bewoning.Data.Authorisation;
using Bewoning.Data.Helpers;
using Bewoning.Data.Options;
using Bewoning.Data.Providers;
using Bewoning.Data.Repositories.Postgres;
using Bewoning.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bewoning.Data;

public static class RegisterServicesExtension
{
    public static void ConfigureRvigRepoDataBaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICurrentDateTimeProvider, DateTimeTodayProvider>();
    }

    public static void ConfigureRvigDataBaseWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WebApiOptions>(configuration.GetSection(WebApiOptions.WebApi));
    }

    public static void ConfigureRvigDataBasePostgresServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.DatabaseSection));

        services.AddSingleton<IAutorisationRepo, AutorisationRepo>();
        services.AddSingleton<IProtocolleringRepo, ProtocolleringRepo>();
        services.AddSingleton<IProtocolleringService, ProtocolleringService>();
        services.AddSingleton<IDomeinTabellenRepo, DbDomeinTabellenRepo>();
        services.AddSingleton<IRvigDbHealthCheckRepo, RvigDbHealthCheckRepo>();
        services.AddSingleton<IDomeinTabellenHelper, DomeinTabellenHelper>();
        services.AddSingleton<IHealthCheckDatabaseConnectionService, HealthCheckDatabaseConnectionService>();
    }
}
