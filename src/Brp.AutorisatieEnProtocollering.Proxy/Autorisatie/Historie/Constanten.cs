using System.Collections.ObjectModel;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Historie;

public static class Constanten
{
    static Constanten()
    {
        FieldElementNrDictionary = new(_fieldElementNrDictionary);
    }

    public static ReadOnlyDictionary<string, string> FieldElementNrDictionary { get; }

    private static readonly Dictionary<string, string> _fieldElementNrDictionary = new()
    {
        { "burgerservicenummer", "010120" },
        { "datumTot", "081030 081320 581030 581320" },
        { "datumVan", "081030 081320 581030 581320" },
        { "peildatum", "081030 081320 581030 581320" },
        { "verblijfplaatshistorie", "PXVP07" }
    };
}
