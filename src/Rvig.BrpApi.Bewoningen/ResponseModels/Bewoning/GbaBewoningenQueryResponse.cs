using System.Runtime.Serialization;
using Rvig.BrpApi.Bewoningen.ApiModels.Bewoning;

namespace Rvig.BrpApi.Bewoningen.ResponseModels.Bewoning
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
