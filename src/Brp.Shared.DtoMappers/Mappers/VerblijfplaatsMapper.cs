namespace Brp.Shared.DtoMappers.Mappers;

public static class VerblijfplaatsMapper
{
  private const string CategorieVerblijfplaats = "080000";
  private const string GroepAdresBuitenland = "081300";
  private const string LandAdresBuitenland = "081310";
  private const string DatumAanvangAdresBuitenland = "081320";
  private const string GroepGeldigheid = "088500";
  private const string DatumIngangGeldigheid = "088510";
  private const string VastgesteldVerblijftNietOpAdres = "089999";

  public static BrpApiDtos.AbstractVerblijfplaats? Map(this BrpDtos.GbaVerblijfplaats? source)
    {
        if (source == null)
        {
            return null;
        }
        if (source.IsOnbekendVerblijfplaatsBuitenland())
        {
            return source.MapVerblijfplaatsOnbekend();
        }
        if (source.IsVerblijfplaatsBuitenland())
        {
            return source.MapVerblijfplaatsBuitenland();
        }
        if (source.IsAdres())
        {
            return source.MapVerblijfplaatsAdres();
        }
        if (source.IsLocatie())
        {
            return source.MapLocatie();
        }

        return null;
    }

    private static bool IsOnbekendVerblijfplaatsBuitenland(this BrpDtos.GbaVerblijfplaats source) =>
        source.Land != null &&
        source.Land.Code == "0000";

    private static BrpApiDtos.VerblijfplaatsOnbekend MapVerblijfplaatsOnbekend(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.VerblijfplaatsOnbekend
        {
            DatumVan = source.MapDatumVan(),
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            InOnderzoek = source.InOnderzoek.VerblijfplaatsOnbekendInOnderzoek()
        };
    }

    private static BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek? VerblijfplaatsOnbekendInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats => new BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumVan = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepAdresBuitenland => new BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek
            {
                DatumVan = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            LandAdresBuitenland => new BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            DatumAanvangAdresBuitenland => new BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek
            {
                DatumVan = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepGeldigheid or
            DatumIngangGeldigheid => new BrpApiDtos.VerblijfplaatsOnbekendInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static bool IsVerblijfplaatsBuitenland(this BrpDtos.GbaVerblijfplaats source) =>
        source.Land != null &&
        source.Land.Code != "0000";

    private static BrpApiDtos.VerblijfplaatsBuitenland MapVerblijfplaatsBuitenland(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.VerblijfplaatsBuitenland
        {
            DatumVan = source.DatumAanvangAdresBuitenland?.Map(),
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            Verblijfadres = source.MapVerblijfadresBuitenland(),
            InOnderzoek = source.InOnderzoek.VerblijfplaatsBuitenlandInOnderzoek()
        };
    }

    private static BrpApiDtos.VerblijfadresBuitenland MapVerblijfadresBuitenland(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.VerblijfadresBuitenland
        {
            Land = source.Land?.Code == "0000" ? null : source.Land?.Map(),
            Regel1 = source.Regel1,
            Regel2 = source.Regel2,
            Regel3 = source.Regel3,
            InOnderzoek = source.InOnderzoek.VerblijfadresBuitenlandInOnderzoek()
        };
    }

    private static BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek? VerblijfplaatsBuitenlandInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats => new BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumVan = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepAdresBuitenland => new BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek
            {
                DatumVan = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            LandAdresBuitenland => new BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            DatumAanvangAdresBuitenland => new BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek
            {
                DatumVan = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepGeldigheid or
            DatumIngangGeldigheid => new BrpApiDtos.VerblijfplaatsBuitenlandInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static BrpApiDtos.VerblijfadresBuitenlandInOnderzoek? VerblijfadresBuitenlandInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats or
            GroepAdresBuitenland => new BrpApiDtos.VerblijfadresBuitenlandInOnderzoek
            {
                Regel1 = true,
                Regel2 = true,
                Regel3 = true,
                Land = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            LandAdresBuitenland => new BrpApiDtos.VerblijfadresBuitenlandInOnderzoek
            {
                Land = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081330" => new BrpApiDtos.VerblijfadresBuitenlandInOnderzoek
            {
                Regel1 = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081340" => new BrpApiDtos.VerblijfadresBuitenlandInOnderzoek
            {
                Regel2 = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081350" => new BrpApiDtos.VerblijfadresBuitenlandInOnderzoek
            {
                Regel3 = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static bool IsAdres(this BrpDtos.GbaVerblijfplaats source) => !string.IsNullOrWhiteSpace(source.Straat);

    private static BrpApiDtos.Adres MapVerblijfplaatsAdres(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.Adres
        {
            DatumVan = source.DatumAanvangAdreshouding?.Map(),
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            FunctieAdres = source.FunctieAdres?.Map(),
            Verblijfadres = source.MapVerblijfadresBinnenland(),
            AdresseerbaarObjectIdentificatie = source.AdresseerbaarObjectIdentificatie != "0000000000000000"
                ? source.AdresseerbaarObjectIdentificatie
                : null,
            NummeraanduidingIdentificatie = source.NummeraanduidingIdentificatie != "0000000000000000"
                ? source.NummeraanduidingIdentificatie
                : null,
            IndicatieVastgesteldVerblijftNietOpAdres = source.InOnderzoek?.AanduidingGegevensInOnderzoek == null
                ? null
                : source.MapVastgesteldVerblijftNietOpAdres(),
            InOnderzoek = source.InOnderzoek.AdresOnderzoek()
        };
    }

    private static BrpApiDtos.AdresInOnderzoek? AdresOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats or
            VastgesteldVerblijftNietOpAdres => new BrpApiDtos.AdresInOnderzoek
            {
                AdresseerbaarObjectIdentificatie = true,
                DatumIngangGeldigheid = true,
                DatumVan = true,
                FunctieAdres = true,
                NummeraanduidingIdentificatie = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081000" => new BrpApiDtos.AdresInOnderzoek
            {
                DatumVan = true,
                FunctieAdres = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081010" => new BrpApiDtos.AdresInOnderzoek
            {
                FunctieAdres = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081030" or
            DatumAanvangAdresBuitenland => new BrpApiDtos.AdresInOnderzoek
            {
                DatumVan = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081100" => new BrpApiDtos.AdresInOnderzoek
            {
                AdresseerbaarObjectIdentificatie = true,
                NummeraanduidingIdentificatie = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081110" => new BrpApiDtos.AdresInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081180" => new BrpApiDtos.AdresInOnderzoek
            {
                AdresseerbaarObjectIdentificatie = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081190" => new BrpApiDtos.AdresInOnderzoek
            {
                NummeraanduidingIdentificatie = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081300" => new BrpApiDtos.AdresInOnderzoek
            {
                DatumVan = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepGeldigheid or
            DatumIngangGeldigheid => new BrpApiDtos.AdresInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static BrpApiDtos.VerblijfadresBinnenland MapVerblijfadresBinnenland(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.VerblijfadresBinnenland
        {
            OfficieleStraatnaam = source.NaamOpenbareRuimte,
            KorteStraatnaam = source.Straat != "." ? source.Straat : null,
            Huisnummer = source.Huisnummer,
            Huisletter = source.Huisletter,
            Huisnummertoevoeging = source.Huisnummertoevoeging,
            AanduidingBijHuisnummer = source.AanduidingBijHuisnummer,
            Postcode = source.Postcode,
            Woonplaats = source.Woonplaats != "." ? source.Woonplaats : null,
            InOnderzoek = source.InOnderzoek.VerblijfadresBinnenlandInOnderzoek()
        };
    }

    private static BrpApiDtos.VerblijfadresBinnenlandInOnderzoek? VerblijfadresBinnenlandInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats or
            "081100" or
            VastgesteldVerblijftNietOpAdres => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                AanduidingBijHuisnummer = true,
                Huisletter = true,
                Huisnummer = true,
                Huisnummertoevoeging = true,
                KorteStraatnaam = true,
                Postcode = true,
                OfficieleStraatnaam = true,
                Woonplaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "080900" or
            "080910" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Woonplaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081110" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                KorteStraatnaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081115" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                OfficieleStraatnaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081120" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Huisnummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081130" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Huisletter = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081140" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Huisnummertoevoeging = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081150" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                AanduidingBijHuisnummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081160" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Postcode = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081170" => new BrpApiDtos.VerblijfadresBinnenlandInOnderzoek
            {
                Woonplaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static bool IsLocatie(this BrpDtos.GbaVerblijfplaats source) => !string.IsNullOrWhiteSpace(source.Locatiebeschrijving);

    private static BrpApiDtos.Locatie MapLocatie(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.Locatie
        {
            DatumVan = source.DatumAanvangAdreshouding?.Map(),
            DatumIngangGeldigheid = source.DatumIngangGeldigheid?.Map(),
            FunctieAdres = source.FunctieAdres?.Map(),
            Verblijfadres = source.MapVerblijfadresLocatie(),
            IndicatieVastgesteldVerblijftNietOpAdres = source.InOnderzoek?.AanduidingGegevensInOnderzoek == null
                ? null
                : source.MapVastgesteldVerblijftNietOpAdres(),
            InOnderzoek = source.InOnderzoek.LocatieInOnderzoek()
        };
    }

    private static BrpApiDtos.LocatieInOnderzoek? LocatieInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats or
            VastgesteldVerblijftNietOpAdres => new BrpApiDtos.LocatieInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumVan = true,
                FunctieAdres = true,
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081000" => new BrpApiDtos.LocatieInOnderzoek
            {
                DatumVan = true,
                FunctieAdres = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081010" => new BrpApiDtos.LocatieInOnderzoek
            {
                FunctieAdres = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081030" => new BrpApiDtos.LocatieInOnderzoek
            {
                DatumVan = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "081200" or
            "081210" => new BrpApiDtos.LocatieInOnderzoek
            {
                Type = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            GroepGeldigheid or
            DatumIngangGeldigheid => new BrpApiDtos.LocatieInOnderzoek
            {
                DatumIngangGeldigheid = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static BrpApiDtos.VerblijfadresLocatie MapVerblijfadresLocatie(this BrpDtos.GbaVerblijfplaats source)
    {
        return new BrpApiDtos.VerblijfadresLocatie
        {
            Locatiebeschrijving = source.Locatiebeschrijving,
            InOnderzoek = source.InOnderzoek.VerblijfadresLocatieInOnderzoek()
        };
    }

    private static BrpApiDtos.VerblijfadresLocatieInOnderzoek? VerblijfadresLocatieInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            CategorieVerblijfplaats or
            "081200" or
            "081210" or
            VastgesteldVerblijftNietOpAdres => new BrpApiDtos.VerblijfadresLocatieInOnderzoek
            {
                Locatiebeschrijving = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    private static BrpApiDtos.AbstractDatum? MapDatumVan(this BrpDtos.GbaVerblijfplaats verblijfplaats)
    {
        return verblijfplaats switch
        {
            { DatumAanvangAdresBuitenland: var datum } when !string.IsNullOrWhiteSpace(datum) => datum.Map(),
            { DatumAanvangAdreshouding: var datum } when !string.IsNullOrWhiteSpace(datum) => datum.Map(),
            _ => null
        };
    }

    private static string? MapStraat(this BrpDtos.GbaVerblijfplaats verblijfplaats)
    {
        return !string.IsNullOrWhiteSpace(verblijfplaats.NaamOpenbareRuimte)
            ? verblijfplaats.NaamOpenbareRuimte
            : verblijfplaats.Straat;
    }

    private static bool? MapVastgesteldVerblijftNietOpAdres(this BrpDtos.GbaVerblijfplaatsBeperkt? verblijfplaats)
    {
        return verblijfplaats?.InOnderzoek?.AanduidingGegevensInOnderzoek == VastgesteldVerblijftNietOpAdres ? true : null;
    }

    private static bool? MapVastgesteldVerblijftNietOpAdres(this BrpDtos.GbaVerblijfplaats? verblijfplaats)
    {
        return verblijfplaats?.InOnderzoek?.AanduidingGegevensInOnderzoek == VastgesteldVerblijftNietOpAdres ? true : null;
    }
}
