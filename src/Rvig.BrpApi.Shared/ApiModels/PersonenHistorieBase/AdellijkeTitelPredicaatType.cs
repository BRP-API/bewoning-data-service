using System.Runtime.Serialization;
using Rvig.BrpApi.Shared.ApiModels.Universal;

namespace Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class AdellijkeTitelPredicaatType : Waardetabel
	{
		/// <summary>
		/// Gets or Sets Soort
		/// </summary>
		[DataMember(Name = "soort", EmitDefaultValue = false)]
		public AdellijkeTitelPredicaatSoort? Soort { get; set; }
	}
}
