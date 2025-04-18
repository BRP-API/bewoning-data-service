using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Rvig.BrpApi.Shared.ApiModels.Universal
{
	/// <summary>
	/// Gets or Sets AdellijkeTitelPredicaatSoort
	/// </summary>
	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum AdellijkeTitelPredicaatSoort
	{
		/// <summary>
		/// Enum TitelEnum for titel
		/// </summary>
		[EnumMember(Value = "titel")]
		TitelEnum = 1,

		/// <summary>
		/// Enum PredicaatEnum for predicaat
		/// </summary>
		[EnumMember(Value = "predicaat")]
		PredicaatEnum = 2
	}
}
