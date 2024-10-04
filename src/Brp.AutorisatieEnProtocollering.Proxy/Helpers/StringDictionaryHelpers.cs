using System.Collections.ObjectModel;

namespace Brp.AutorisatieEnProtocollering.Proxy.Helpers;

public static class StringDictionaryHelpers
{
    public static IEnumerable<(string Name, string[] Value)> ToKeyStringArray(this IEnumerable<string> keys, ReadOnlyDictionary<string, string> keyValuePairs, string zoekType, BepaalKeyVoor bepaalKeyVoor)
    {
        var retval = new List<(string Name, string[] Value)>();

        foreach (var key in keys)
        {
            var sleutel = bepaalKeyVoor(key, zoekType);

            var fieldElementNrs = keyValuePairs.ContainsKey(sleutel)
                ? keyValuePairs[sleutel]
                : keyValuePairs[key];

            retval.Add(new(key, !string.IsNullOrWhiteSpace(fieldElementNrs) ? fieldElementNrs.Split(' ') : Array.Empty<string>()));
        }

        return retval;
    }

    public delegate string BepaalKeyVoor(string field, string zoekType);
}
