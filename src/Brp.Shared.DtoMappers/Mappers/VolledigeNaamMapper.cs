using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.Mappers;

public static class VolledigeNaamMapper
{
    public static string? VolledigeNaam(this INaamBasis naam, IWaardetabel? geslacht)
    {
        var adellijkeTitel = naam.AdellijkeTitelPredicaat.MapNaarAdellijkeTitel(geslacht);
        var predikaat = naam.AdellijkeTitelPredicaat.MapNaarPredicaat(geslacht);
        var geslachtsnaam = !string.IsNullOrWhiteSpace(naam.Geslachtsnaam) && !naam.Geslachtsnaam.Equals(".") ? naam.Geslachtsnaam : string.Empty;

        var retval = $"{predikaat} {naam.Voornamen} {adellijkeTitel} {naam.Voorvoegsel} {geslachtsnaam}".RemoveRedundantSpaces();

        return !string.IsNullOrWhiteSpace(retval)
            ? retval
            : null;
    }
}
