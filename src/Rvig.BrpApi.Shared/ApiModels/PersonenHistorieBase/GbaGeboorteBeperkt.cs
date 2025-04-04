using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class GbaGeboorteBeperkt
	{
		/// <summary>
		/// Gets or Sets Datum
		/// </summary>
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datum", EmitDefaultValue = false)]
		public string? Datum { get; set; }

		[JsonIgnore]
		[XmlIgnore]
		public int? DatumJaar => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length >= 4 ? int.Parse(Datum.Substring(0, 4)) : null;

		[JsonIgnore]
		[XmlIgnore]
		public int? DatumMaand => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length >= 6 ? int.Parse(Datum.Substring(4, 2)) : null;

		[JsonIgnore]
		[XmlIgnore]
		public int? DatumDag => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length == 8 ? int.Parse(Datum.Substring(6, 2)) : null;
	}
}
