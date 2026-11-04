using Brp.Shared.DtoMappers.BrpApiDtos;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Brp.Shared.DtoMappers.Mappers;

public static class DatumMapper
{
    private static readonly Regex GbaDatumRegex = new("^(?<jaar>[0-9]{4})(?<maand>[0-9]{2})(?<dag>[0-9]{2})$", RegexOptions.None, TimeSpan.FromMilliseconds(100));
    private static readonly Dictionary<int, string> MaandDictionary = new()
    {
        {1, "januari" },
        {2, "februari" },
        {3, "maart" },
        {4, "april" },
        {5, "mei" },
        {6, "juni" },
        {7, "juli" },
        {8, "augustus" },
        {9, "september" },
        {10, "oktober" },
        {11, "november" },
        {12, "december" }
    };

    private static DatumOnbekend CreateDatumOnbekend() => new() { Onbekend = true, LangFormaat = "onbekend" };
    private static VolledigeDatum CreateVolledigeDatum(int jaar, int maand, int dag) => new()
    {
        Datum = new DateTime(jaar, maand, dag, 0, 0, 0, DateTimeKind.Local),
        LangFormaat = $"{dag} {MaandDictionary[maand]} {jaar}"
    };
    private static JaarMaandDatum CreateJaarMaandDatum(int jaar, int maand) => new()
    {
        Jaar = jaar,
        Maand = maand,
        LangFormaat = $"{MaandDictionary[maand]} {jaar}"
    };
    private static JaarDatum CreateJaarDatum(int jaar) => new()
    {
        Jaar = jaar,
        LangFormaat = $"{jaar}"
    };

    public static AbstractDatum Map(this string datum)
    {
        if (!GbaDatumRegex.IsMatch(datum))
        {
            return CreateDatumOnbekend();
        }

        var match = GbaDatumRegex.Match(datum);
        var jaar = int.Parse(match.Groups["jaar"].Value, CultureInfo.InvariantCulture);
        var maand = int.Parse(match.Groups["maand"].Value, CultureInfo.InvariantCulture);
        var dag = int.Parse(match.Groups["dag"].Value, CultureInfo.InvariantCulture);

        return (jaar, maand, dag) switch
        {
            (0, 0, 0) => CreateDatumOnbekend(),
            (_, 0, 0) => CreateJaarDatum(jaar),
            (_, _, 0) => CreateJaarMaandDatum(jaar, maand),
            _ => CreateVolledigeDatum(jaar, maand, dag),
        };
    }
}
