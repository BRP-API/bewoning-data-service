using Brp.Shared.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using Serilog;
using Brp.Shared.Infrastructure.ProblemDetails;
using Bewoning.Data.Service.Middleware;
using Bewoning.Data.Service.Services;
using Bewoning.Api.Options;
using Bewoning.Api;

namespace Bewoning.Data.Service;

public static class RvigBaseApp
{
    /// <summary>
    /// Init app
    /// </summary>
    /// <param name="servicesToConfigure">Services to add as singletons. Key is interface, value is implementation.</param>
    public static void Init(IDictionary<Type, Type> servicesToConfigure, string apiName)
    {
        Log.Logger = SerilogHelpers.SetupSerilogBootstrapLogger();

        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            builder.SetupSerilog(Log.Logger);

            var appsettingsJsonFileName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower().Equals("development") == true
                ? "appsettings.Development.json"
                : "appsettings.json";

            builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appsettingsJsonFileName, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.ConfigureRvigRepoDataBaseServices(builder.Configuration);
            builder.Services.ConfigureRvigDataBasePostgresServices(builder.Configuration);
            builder.Services.ConfigureRvigApiSharedServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            ConfigureServices(builder.Services, builder.Configuration);

            // Loading services from child app. Personen, Reisdocumenten, Bewoningen or Historie.
            foreach (var servicePair in servicesToConfigure)
            {
                builder.Services.AddSingleton(servicePair.Key, servicePair.Value);
            }

            builder.Services.AddControllers(options =>
            {
                // Removes the POST main body mentioned during required errors.
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddNewtonsoftJson();

            builder.Services.Configure<MvcOptions>(options => options.Filters.Add(new ProducesAttribute(Application.Json)));

            var app = builder.Build();

            app.SetupSerilogRequestLogging();
            app.UseMiddleware<UnhandledExceptionHandler>();

            app.UseRouting();

            // So we can grab the POST body to validate for unknown params. See ValidateUnusableQueryParamsAttribute.cs
            app.UseMiddleware<EnableRequestBodyBufferingMiddleware>();

            // It is a requirement of the GBA API to default to application/json when Content-Type isn't specified.
            app.UseMiddleware<ForceAcceptAndContentTypeHeadersWithValueMiddleware>();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{ApiName} terminated unexpectedly.", apiName);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(IServiceCollection services, ConfigurationManager configurationManager)
    {
        // Create a static manager to use in other classes where you cannot use IOptions in the constructor. Example being the ValidateVersionHeaderAttribute
        AppSettingsManager.Configuration = configurationManager;

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHealthCheckApiService, HealthCheckApiService>();
    }
}
