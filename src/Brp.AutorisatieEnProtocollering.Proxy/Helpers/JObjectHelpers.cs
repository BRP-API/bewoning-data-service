using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Brp.AutorisatieEnProtocollering.Proxy.Helpers;

public static class JObjectHelpers
{

    public static IEnumerable<(string Name, string[] Value)> BepaalElementNrVanZoekParameters(this JObject input, ReadOnlyDictionary<string, string> fieldElementNrDictionary)
    {
        return from property in input.Properties()
               where !new[] { "type", "fields", "inclusiefOverledenPersonen" }.Contains(property.Name)
               select (property.Name, Value: fieldElementNrDictionary[property.Name].Split(' '));
    }

    public static IEnumerable<string> WaardeFieldsParameter(this JObject input)
    {
        return from field in input.Value<JArray>("fields")!
               let v = field.Value<string>()
               where v != null
               select v;
    }

    public static string? WaardeTypeParameter(this JObject input) => input.Value<string>("type");
}