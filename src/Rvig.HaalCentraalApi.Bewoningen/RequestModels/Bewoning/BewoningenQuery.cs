using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using Rvig.HaalCentraalApi.Bewoningen.Util;

namespace Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning
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
