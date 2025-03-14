using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Rvig.BrpApi.Shared.ApiModels.Universal;

namespace Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase
{
	/// <summary>
	/// Gegevens over de verblijfsrechtelijke status van de persoon. * **datumEinde**: Datum waarop de geldigheid van de gegevens over de verblijfstitel is beÃ«indigd. * **datumIngang**: Datum waarop de gegevens over de verblijfstitel geldig zijn geworden. * **aanduiding** : Verblijfstiteltabel die aangeeft over welke verblijfsrechtelijke status de persoon beschikt.
	/// </summary>
	[DataContract]
	public class GbaVerblijfstitel
	{
		/// <summary>
		/// Gets or Sets Aanduiding
		/// </summary>
		[DataMember(Name = "aanduiding", EmitDefaultValue = false)]
		public Waardetabel? Aanduiding { get; set; }

		/// <summary>
		/// Gets or Sets DatumEinde
		/// </summary>
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datumEinde", EmitDefaultValue = false)]
		public string? DatumEinde { get; set; }

		/// <summary>
		/// Gets or Sets DatumIngang
		/// </summary>
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datumIngang", EmitDefaultValue = false)]
		public string? DatumIngang { get; set; }

		/// <summary>
		/// Gets or Sets InOnderzoek
		/// </summary>
		[DataMember(Name = "inOnderzoek", EmitDefaultValue = false)]
		public GbaInOnderzoek? InOnderzoek { get; set; }

		[JsonIgnore]
		[XmlIgnore]
		public string? _datumOpneming { get; set; }
	}
}
