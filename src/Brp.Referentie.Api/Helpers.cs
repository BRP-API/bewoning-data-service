using Newtonsoft.Json.Linq;

namespace Brp.Referentie.Api;

public static class Helpers
{
    public static async Task AddCustomResponseHeaders(this HttpResponse response, IWebHostEnvironment environment)
    {
        var path = Path.Combine(environment.ContentRootPath, "Data", "response-headers.json");
        if (File.Exists(path))
        {
            var data = await File.ReadAllTextAsync(path);

            var responseHeaders = JObject.Parse(data);
            foreach (var header in responseHeaders)
            {
                response.Headers.Add(header.Key, header.Value?.ToString());
            }

        }
    }

    public static async Task<bool> AddCustomResponseBody(this HttpResponse response, IWebHostEnvironment environment)
    {
        var path = Path.Combine(environment.ContentRootPath, "Data", "response-body.json");
        if (File.Exists(path))
        {
            var data = await File.ReadAllBytesAsync(path);

            await response.Body.WriteAsync(data);

            return true;
        }

        return false;
    }
}
