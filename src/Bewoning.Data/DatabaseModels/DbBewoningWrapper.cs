using Bewoning.Data.Authorisation;

namespace Bewoning.Data.DatabaseModels;

/// <summary>
/// Combine all different database persoon parts that represent one haalcentraal ingeschreven persoon into one class.
/// </summary>
public class DbBewoningWrapper
{
    [RubriekElement("09.10")] public short? inschrijving_gemeente_code { get; set; }
    [RubriekElement("11.80")] public string? verblijf_plaats_ident_code { get; init; }
    [RubriekCategory(1, 51)] public IEnumerable<bewoning_bewoner> Bewoners { get; set; }

    public DbBewoningWrapper()
    {
        Bewoners = new List<bewoning_bewoner>();
    }
}
