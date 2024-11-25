using Rvig.BrpApi.Bewoningen.ApiModels.Bewoning;
using Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase;

namespace Test.RvigHaalCentraal.Util;

public static class GbaBewoningFixture
{
    public static GbaBewoning CreateDefaultWithBewoner(GbaBewoner bewoner)
    {
        return new GbaBewoning
        {
            AdresseerbaarObjectIdentificatie = "0000000000000000",
            Periode = new Periode
            {
                DatumVan = "20240101",
                DatumTot = "20240101"
            },
            Bewoners = new List<GbaBewoner>
            {
                bewoner
            },
            MogelijkeBewoners = new List<GbaBewoner>(),
            IndicatieVeelBewoners = false
        };
    }

    public static GbaBewoning CreateDefaultWithAdresseerbaarObjectAndBewoner(string adresseerbaarObjectIdentificatie, GbaBewoner bewoner)
    {
        return new GbaBewoning
        {
            AdresseerbaarObjectIdentificatie = adresseerbaarObjectIdentificatie,
            Periode = new Periode
            {
                DatumVan = "20240101",
                DatumTot = "20240101"
            },
            Bewoners = new List<GbaBewoner>
            {
                bewoner
            },
            MogelijkeBewoners = new List<GbaBewoner>(),
            IndicatieVeelBewoners = false
        };
    }

    
}