using Rvig.BrpApi.Shared.Fields;

namespace Rvig.BrpApi.Bewoningen.Fields;
public class BewoningenFieldsSettings : FieldsSettings
{
	public override FieldsSettingsModel GbaFieldsSettings { get; }

	public BewoningenFieldsSettings()
	{
		GbaFieldsSettings = InitGbaFieldsSettings();
	}

	protected override FieldsSettingsModel InitGbaFieldsSettings()
	{
		return new FieldsSettingsModel("fields")
		{
			ForbiddenProperties = new List<string>(),
			PropertiesToDiscard = new List<string>(),
			MandatoryProperties = new List<string> { "periode" },
			SetChildPropertiesIfExistInScope = new Dictionary<string, string> { { "periode", "" } },
			SetPropertiesIfContextPropertyNotNull = new Dictionary<string, string> { { "periode.datumVan", "periode" }, { "periode.datumTot", "periode" }, },
			ShortHandMappings = new Dictionary<string, string>()
		};
	}
}