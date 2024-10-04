using System.Collections.ObjectModel;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Reisdocumenten;

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

        { "soort", "123510" },
        { "soort.code", "123510" },
        { "soort.omschrijving", "123510" },
        { "reisdocumentnummer", "123520" },
        { "datumEindeGeldigheid", "123550" },
        { "datumEindeGeldigheid.type", "123550" },
        { "datumEindeGeldigheid.datum", "123550" },
        { "datumEindeGeldigheid.langFormaat", "123550" },
        { "datumEindeGeldigheid.onbekend", "123550" },
        { "datumEindeGeldigheid.jaar", "123550" },
        { "datumEindeGeldigheid.maand", "123550" },
        { "inhoudingOfVermissing", "123560 123570" },
        { "inhoudingOfVermissing.datum", "123560" },
        { "inhoudingOfVermissing.datum.type", "123560" },
        { "inhoudingOfVermissing.datum.datum", "123560" },
        { "inhoudingOfVermissing.datum.langFormaat", "123560" },
        { "inhoudingOfVermissing.datum.onbekend", "123560" },
        { "inhoudingOfVermissing.datum.jaar", "123560" },
        { "inhoudingOfVermissing.datum.maand", "123560" },
        { "inhoudingOfVermissing.aanduiding", "123570" },
        { "inhoudingOfVermissing.aanduiding.code", "123570" },
        { "inhoudingOfVermissing.aanduiding.omschrijving", "123570" },
        { "houder", "010120" },
        { "houder.burgerservicenummer", "010120" },
    };
}