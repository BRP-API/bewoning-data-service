using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;

namespace Rvig.BrpApi.Bewoningen.Util;

public class BewoningenQueryJsonInheritanceConverter : QueryBaseJsonInheritanceConverter
{
	public BewoningenQueryJsonInheritanceConverter()
	{
	}

	public BewoningenQueryJsonInheritanceConverter(string discriminatorName) : base(discriminatorName)
	{
	}

	public BewoningenQueryJsonInheritanceConverter(Type baseType) : base(baseType)
	{
	}

	public BewoningenQueryJsonInheritanceConverter(string discriminatorName, bool readTypeProperty) : base(discriminatorName, readTypeProperty)
	{
	}

	public BewoningenQueryJsonInheritanceConverter(Type baseType, string discriminatorName) : base(baseType, discriminatorName)
	{
	}

	public BewoningenQueryJsonInheritanceConverter(Type baseType, string discriminatorName, bool readTypeProperty) : base(baseType, discriminatorName, readTypeProperty)
	{
	}

	protected override List<string> _subTypes => new()
		{
			nameof(BewoningMetPeildatum),
			nameof(BewoningMetPeriode),
			//nameof(MedebewonersMetPeildatum),
			//nameof(MedebewonersMetPeriode)
		};
	protected override string _discriminator => nameof(BewoningenQuery.type);
}