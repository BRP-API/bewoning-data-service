using Rvig.Data.Base.Authorisation;

namespace Rvig.Data.Bewoningen.DatabaseModels;

/// <summary>
/// Combine all different database persoon parts that represent one haalcentraal ingeschreven persoon into one class.
/// </summary>
public record BewoningOnBsnWrapper
{
	public long pl_id { get; set; }
	[RubriekCategory(08, 58), RubriekElement("11.80")] public string? verblijf_plaats_ident_code { get; init; }
	[RubriekCategory(08, 58), RubriekElement("10.30")] public int? begin_date { get; set; }
	[RubriekCategory(08, 58), RubriekElement("10.30")] public int? end_date { get; set; }
}