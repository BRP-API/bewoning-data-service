using System.Runtime.Serialization;
using Rvig.BrpApi.Shared.ApiModels.Universal;

namespace Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class GbaGeboorte : GbaGeboorteBeperkt
	{
		/// <summary>
		/// Gets or Sets Land
		/// </summary>
		[DataMember(Name = "land", EmitDefaultValue = false)]
		public Waardetabel? Land { get; set; }

		/// <summary>
		/// Gets or Sets Plaats
		/// </summary>
		[DataMember(Name = "plaats", EmitDefaultValue = false)]
		public Waardetabel? Plaats { get; set; }
	}
}
