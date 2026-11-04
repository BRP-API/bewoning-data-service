using Brp.Shared.DtoMappers.BrpApiDtos;
using System.Collections.ObjectModel;

namespace Brp.Shared.DtoMappers.Mappers;

public static class NationaliteitMapper
{
    private const string CategorieNationaliteit = "040000";
    private const string GroepNationaliteit = "040500";
    private const string Nationaliteit = "040510";
    private const string GroepOpnemenNationaliteit = "046300";
    private const string RedenOpnameNationaliteit = "046310";

  public static Collection<AbstractNationaliteit>? Map(this ICollection<BrpDtos.GbaNationaliteit> nationaliteiten)
    {
        var retval = new Collection<AbstractNationaliteit>();
        foreach (var nationaliteit in from nationaliteit in nationaliteiten
                                      where nationaliteit != null
                                      select nationaliteit)
        {
          retval.Add(nationaliteit.Map()!);
        }

        return retval;
    }

    public static AbstractNationaliteit? Map(this BrpDtos.GbaNationaliteit? nationaliteit)
    {
        if (nationaliteit == null)
        {
            return null;
        }
        return nationaliteit switch
        {
            { Nationaliteit.Code: var code } when code == "0002" => nationaliteit.MapBehandeldAlsNederlander(),
            { Nationaliteit.Code: var code } when code == "0500" => nationaliteit.MapVastgesteldNietNederlander(),
            { Nationaliteit.Code: var code } when code == "0499" => nationaliteit.MapStaatloos(),
            { Nationaliteit.Code: var code } when code != "0000" => nationaliteit.MapNationaliteitBekend(),
            { AanduidingBijzonderNederlanderschap: var code } when code == "B" => nationaliteit.MapBehandeldAlsNederlander(),
            { AanduidingBijzonderNederlanderschap: var code } when code == "V" => nationaliteit.MapVastgesteldNietNederlander(),
            _ => nationaliteit.MapNationaliteitOnbekend()
        };
    }

    private static BehandeldAlsNederlander MapBehandeldAlsNederlander(this BrpDtos.GbaNationaliteit source)
    {
        return new BehandeldAlsNederlander
        {
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            RedenOpname = source.RedenOpname?.Code == "000" ? null : source.RedenOpname.Map(),
            InOnderzoek = source.InOnderzoek.BijzonderNederlanderschapInOnderzoek()
        };
    }

    private static BijzonderNederlanderschapInOnderzoek? BijzonderNederlanderschapInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieNationaliteit => new BrpApiDtos.BijzonderNederlanderschapInOnderzoek
            {
                RedenOpname = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepNationaliteit or
            Nationaliteit or
            "046500" or
            "046510" => new BrpApiDtos.BijzonderNederlanderschapInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            GroepOpnemenNationaliteit or
            RedenOpnameNationaliteit => new BrpApiDtos.BijzonderNederlanderschapInOnderzoek
            {
                RedenOpname = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            _ => null
        };
    }

    private static VastgesteldNietNederlander MapVastgesteldNietNederlander(this BrpDtos.GbaNationaliteit source)
    {
        return new VastgesteldNietNederlander
        {
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            RedenOpname = source.RedenOpname?.Code == "000" ? null : source.RedenOpname.Map(),
            InOnderzoek = source.InOnderzoek.BijzonderNederlanderschapInOnderzoek()
        };
    }

    private static Staatloos MapStaatloos(this BrpDtos.GbaNationaliteit source)
    {
        return new Staatloos
        {
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            RedenOpname = source.RedenOpname?.Code == "000" ? null : source.RedenOpname.Map(),
            InOnderzoek = source.InOnderzoek.StaatloosInOnderzoek()
        };
    }

    private static StaatloosInOnderzoek? StaatloosInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieNationaliteit => new StaatloosInOnderzoek
            {
                RedenOpname = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepNationaliteit or
            Nationaliteit or
            "046500" or
            "046510" => new StaatloosInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            GroepOpnemenNationaliteit or
            RedenOpnameNationaliteit => new StaatloosInOnderzoek
            {
                RedenOpname = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            _ => null
        };
    }

    private static NationaliteitBekend MapNationaliteitBekend(this BrpDtos.GbaNationaliteit source)
    {
        return new NationaliteitBekend
        {
            Nationaliteit = source.Nationaliteit?.Map(),
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            RedenOpname = source.RedenOpname?.Code == "000" ? null : source.RedenOpname.Map(),
            InOnderzoek = source.InOnderzoek.NationaliteitInOnderzoek()
        };
    }

    private static NationaliteitBekendInOnderzoek? NationaliteitInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieNationaliteit => new NationaliteitBekendInOnderzoek
            {
                Nationaliteit = true,
                RedenOpname = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepNationaliteit or Nationaliteit => new NationaliteitBekendInOnderzoek
            {
                Nationaliteit = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            GroepOpnemenNationaliteit or
            RedenOpnameNationaliteit => new NationaliteitBekendInOnderzoek
            {
                RedenOpname = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()

            },
            _ => null
        };
    }

    private static NationaliteitOnbekend MapNationaliteitOnbekend(this BrpDtos.GbaNationaliteit source)
    {
        return new NationaliteitOnbekend
        {
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            RedenOpname = source.RedenOpname?.Code == "000" ? null : source.RedenOpname.Map(),
            InOnderzoek = source.InOnderzoek.NationaliteitOnbekendInOnderzoek()
        };
    }

    private static NationaliteitOnbekendInOnderzoek? NationaliteitOnbekendInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieNationaliteit => new NationaliteitOnbekendInOnderzoek
            {
                RedenOpname = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepNationaliteit or Nationaliteit => new NationaliteitOnbekendInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepOpnemenNationaliteit or
            RedenOpnameNationaliteit => new NationaliteitOnbekendInOnderzoek
            {
                RedenOpname = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null
        };
    }
}
