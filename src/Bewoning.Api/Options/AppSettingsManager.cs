using Microsoft.Extensions.Configuration;

namespace Bewoning.Api.Options;
public static class AppSettingsManager
{
    public static IConfiguration? Configuration { get; set; }
}