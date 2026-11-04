using Brp.Shared.DtoMappers.BrpApiDtos;
using System.Collections.ObjectModel;

namespace Brp.Shared.DtoMappers.Mappers;

public static class KindMapper
{
    public static Collection<Kind>? Map(this ICollection<BrpDtos.GbaKind> kinderen)
    {
        var retval = new Collection<Kind>();
        foreach (var kind in from kind in kinderen
                             where kind != null
                             select kind)
        {
          retval.Add(kind.Map()!);
        }

        return retval;
    }

    public static BrpApiDtos.Kind? Map(this BrpDtos.GbaKind? kind)
    {
        return kind == null
            ? null
            : new BrpApiDtos.Kind
            {
                Naam = kind.Naam.MapNaamGerelateerde(kind.InOnderzoek),
                Geboorte = kind.Geboorte.Map(kind.InOnderzoek),
                Burgerservicenummer = kind.Burgerservicenummer,
                InOnderzoek = kind.InOnderzoek.KindInOnderzoek()
            };
    }
    public static BrpApiDtos.KindInOnderzoek? KindInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "090000" or
            "090100" or
            "090120" => new BrpApiDtos.KindInOnderzoek
            {
                Burgerservicenummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }
}
