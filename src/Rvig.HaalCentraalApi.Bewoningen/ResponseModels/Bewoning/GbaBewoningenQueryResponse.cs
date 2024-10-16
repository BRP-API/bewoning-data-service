using Rvig.HaalCentraalApi.Bewoningen.ApiModels.Bewoning;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Bewoningen.ResponseModels.Bewoning
{
	[DataContract]
	public class GbaBewoningenQueryResponse
	{
		/// <summary>
		/// Gets or Sets Bewoningen
		/// </summary>
		[DataMember(Name = "bewoningen", EmitDefaultValue = false)]
		public List<GbaBewoning>? Bewoningen { get; set; }
	}
}
