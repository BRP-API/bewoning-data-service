using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.Mappers;

public static class AdellijkeTitelPredicaatHelpers
{
    private const string BARONES = "barones";
    private const string BARON = "baron";
    private const string GRAVIN = "gravin";
    private const string GRAAF = "graaf";
    private const string HERTOGIN = "hertogin";
    private const string HERTOG = "hertog";
    private const string MARKIEZIN = "markiezin";
    private const string MARKIES = "markies";
    private const string PRINSES = "prinses";
    private const string PRINS = "prins";
    private const string RIDDER = "ridder";

    private const string JONKVROUW = "jonkvrouw";
    private const string JONKHEER = "jonkheer";

    private static string MapNaarBaron(this IWaardetabel? geslacht) => geslacht.IsVrouw() ? BARONES : BARON;
    private static string MapNaarBarones(this IWaardetabel? geslacht) => geslacht.IsMan() ? BARON : BARONES;
    private static string MapNaarGraaf(this IWaardetabel? geslacht) => geslacht.IsVrouw() ? GRAVIN : GRAAF;
    private static string MapNaarGravin(this IWaardetabel? geslacht) => geslacht.IsMan() ? GRAAF : GRAVIN;
    private static string MapNaarHertog(this IWaardetabel? geslacht) => geslacht.IsVrouw() ? HERTOGIN : HERTOG;
    private static string MapNaarHertogin(this IWaardetabel? geslacht) => geslacht.IsMan() ? HERTOG : HERTOGIN;
    private static string MapNaarMarkies(this IWaardetabel? geslacht) => geslacht.IsVrouw() ? MARKIEZIN : MARKIES;
    private static string MapNaarMarkiezin(this IWaardetabel? geslacht) => geslacht.IsMan() ? MARKIES : MARKIEZIN;
    private static string MapNaarPrins(this IWaardetabel? geslacht) => geslacht.IsVrouw() ? PRINSES : PRINS;
    private static string MapNaarPrinses(this IWaardetabel? geslacht) => geslacht.IsMan() ? PRINS : PRINSES;

    /// <summary>
    /// Mapt de adellijke titel code naar de bijbehorende omschrijving, rekening houdend met het geslacht.
    /// Bij onbekend geslacht wordt de omschrijving horende bij de code gebruikt.
    /// Bij adellijke titel code "R" (ridder) wordt altijd "ridder" gebruikt.
    /// </summary>
    /// <param name="adellijkeTitel"></param>
    /// <param name="geslacht"></param>
    /// <returns></returns>
    public static string MapNaarAdellijkeTitel(this IAdellijkeTitelPredicaatType? adellijkeTitel, IWaardetabel? geslacht)
    {
        if (adellijkeTitel == null) return string.Empty;

        return adellijkeTitel.Code switch
        {
            "B" => geslacht.MapNaarBaron(),
            "BS" => geslacht.MapNaarBarones(),
            "G" => geslacht.MapNaarGraaf(),
            "GI" => geslacht.MapNaarGravin(),
            "H" => geslacht.MapNaarHertog(),
            "HI" => geslacht.MapNaarHertogin(),
            "M" => geslacht.MapNaarMarkies(),
            "MI" => geslacht.MapNaarMarkiezin(),
            "P" => geslacht.MapNaarPrins(),
            "PS" => geslacht.MapNaarPrinses(),
            "R" => RIDDER,
            _ => string.Empty,
        };
    }

    /// <summary>
    /// Mapt de predicaat code naar de bijbehorende omschrijving, rekening houdend met het geslacht.
    /// Bij onbekend geslacht wordt de omschrijving horende bij de code gebruikt.
    /// </summary>
    /// <param name="predicaat"></param>
    /// <param name="geslacht"></param>
    /// <returns></returns>
    public static string MapNaarPredicaat(this IAdellijkeTitelPredicaatType? predicaat, IWaardetabel? geslacht)
    {
        if (predicaat == null) return string.Empty;

        return predicaat.Code switch
        {
            "JH" => geslacht.IsVrouw() ? JONKVROUW : JONKHEER,
            "JV" => geslacht.IsMan() ? JONKHEER : JONKVROUW,
            _ => string.Empty,
        };
    }
}
