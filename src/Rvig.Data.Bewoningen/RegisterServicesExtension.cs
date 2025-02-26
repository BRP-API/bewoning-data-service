using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rvig.BrpApi.Bewoningen.Interfaces;
using Rvig.BrpApi.Bewoningen.Options;
using Rvig.Data.Bewoningen.Authorisation;
using Rvig.Data.Bewoningen.Helpers;
using Rvig.Data.Bewoningen.Options;
using Rvig.Data.Bewoningen.Providers;
using Rvig.Data.Bewoningen.Repositories;
using Rvig.Data.Bewoningen.Services;

namespace Rvig.Data.Bewoningen;

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
