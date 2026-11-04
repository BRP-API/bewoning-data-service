using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.Mappers;

public static class GeslachtHelpers
{
    public static bool IsVrouw(this IWaardetabel? geslacht) => geslacht?.Code.ToUpperInvariant() == "V";

    public static bool IsMan(this IWaardetabel? geslacht) => geslacht?.Code.ToUpperInvariant() == "M";
}
