using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using Rvig.BrpApi.Bewoningen.Util;

namespace Rvig.BrpApi.Bewoningen.RequestModels.Bewoning
{
	[DataContract]
	[JsonConverter(typeof(BewoningenQueryJsonInheritanceConverter), "type")]
	[JsonInheritance("BewoningMetPeildatum", typeof(BewoningMetPeildatum))]
	[JsonInheritance("BewoningMetPeriode", typeof(BewoningMetPeriode))]
	//[JsonInheritance("MedebewonersMetPeildatum", typeof(MedebewonersMetPeildatum))]
	//[JsonInheritance("MedebewonersMetPeriode", typeof(MedebewonersMetPeriode))]
	public class BewoningenQuery
	{
		/// <summary>
		/// Gets or Sets Type
		/// </summary>
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string? type { get; set; }
	}
}
