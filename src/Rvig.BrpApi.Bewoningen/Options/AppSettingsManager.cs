using Microsoft.Extensions.Configuration;

namespace Rvig.BrpApi.Bewoningen.Options;
public static class AppSettingsManager
{
	public static IConfiguration? Configuration { get; set; }
}