using Bewoning.Api.Util;

namespace Bewoning.Api.Validation;
public static class ValidationHelperBase
{
    public static bool IsPeildatumBetweenStartAndEndDates(DateTime? peildatum, string? startString, string? endString)
    {
        var start = CreateDatumOnvolledig(startString);
        var end = CreateDatumOnvolledig(endString);

        var appliesOnStart = peildatum == null || start?.IsBefore(peildatum.Value) != false || start.IsOn(peildatum.Value);
        var appliesOnEnd = peildatum == null || end?.IsAfter(peildatum.Value) != false || end.OnlyYearHasValue() && end.IsOn(peildatum.Value);

        return appliesOnStart && appliesOnEnd;
    }

    public static bool TimePeriodesOverlap(DateTime? datumVan, DateTime? datumTot, string? startString, string? endString)
    {
        var start = CreateDatumOnvolledig(startString);
        var end = CreateDatumOnvolledig(endString);

        var datumVanApplies = datumVan == null || end?.IsAfter(datumVan.Value) != false || end.OnlyYearHasValue() && end.IsOn(datumVan.Value);
        var datumTotApplies = datumTot == null || start?.IsBefore(datumTot.Value) != false || start.IsOn(datumTot.Value);

        return datumVanApplies && datumTotApplies;
    }

    public static DatumOnvolledig? CreateDatumOnvolledig(string? date) => !string.IsNullOrWhiteSpace(date) ? new DatumOnvolledig(date) : null;
}