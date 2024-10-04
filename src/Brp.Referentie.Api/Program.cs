using Brp.Referentie.Api.Middleware;
using Brp.Shared.Infrastructure.Logging;
using Brp.Shared.Infrastructure.ProblemDetails;
using Brp.Shared.Infrastructure.Utils;
using Serilog;

Log.Logger = SerilogHelpers.SetupSerilogBootstrapLogger();

try
{
    Log.Information($"Starting BRP API Referentie Implementatie v{AssemblyHelpers.Version}");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpContextAccessor();

    builder.SetupSerilog(Log.Logger);

    builder.Services.AddControllers()
                    .AddNewtonsoftJson();

    var app = builder.Build();

    app.SetupSerilogRequestLogging();

    app.UseMiddleware<UnhandledExceptionHandler>();

    app.UseMiddleware<SetDefaultHeadersMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "BRP API Referentie Implementatie terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
