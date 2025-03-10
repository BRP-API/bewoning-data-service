using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Rvig.BrpApi.Bewoningen.ApiModels.Bewoning
{
	/// <summary>
	/// Bewoning van een adresseerbaar object
	/// </summary>
	[DataContract]
    public class GbaBewoning
	{
		/// <summary>
		/// De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn.
		/// </summary>
		/// <value>De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn. </value>
		[RegularExpression("^[0-9]{16}$")]
        [DataMember(Name = "adresseerbaarObjectIdentificatie", EmitDefaultValue = false)]
        public string? AdresseerbaarObjectIdentificatie { get; set; }

		/// <summary>
		/// Gets or Sets Periode
		/// </summary>
		[DataMember(Name = "periode", EmitDefaultValue = false)]
		public Periode? Periode { get; set; }

		/// <summary>
		/// Gets or Sets Bewoners
		/// </summary>
		[DataMember(Name = "bewoners", EmitDefaultValue = false)]
		public List<GbaBewoner>? Bewoners { get; set; }

		/// <summary>
		/// Personen waarbij de datum aanvang of de datum einde van de bewoning geheel of gedeeltelijk onbekend is, waardoor niet zeker is of ze in deze periode bewoner waren.
		/// </summary>
		/// <value>Personen waarbij de datum aanvang of de datum einde van de bewoning geheel of gedeeltelijk onbekend is, waardoor niet zeker is of ze in deze periode bewoner waren. </value>
		[DataMember(Name = "mogelijkeBewoners", EmitDefaultValue = false)]
		public List<GbaBewoner>? MogelijkeBewoners { get; set; }

		/// <summary>
		/// Geeft aan dat het adresseerbaar object zo veel bewoners heeft of had in de gevraagde periode dat zij niet in het antwoord worden opgenomen, met uitzondering van de persoon waarvan de BSN is opgegeven.
		/// </summary>
		/// <value>Geeft aan dat het adresseerbaar object zo veel bewoners heeft of had in de gevraagde periode dat zij niet in het antwoord worden opgenomen, met uitzondering van de persoon waarvan de BSN is opgegeven.</value>
		[DataMember(Name = "indicatieVeelBewoners", EmitDefaultValue = false)]
		public bool? IndicatieVeelBewoners { get; set; }

		// Used for protocollering. DO NOT SERIALIZE AS PART OF THE API RESPONSE.
		[XmlIgnore, JsonIgnore]
		public List<(GbaBewoner gbaBewoner, long plId)>? BewonersPlIds { get; set; }

		[XmlIgnore, JsonIgnore]
		public List<(GbaBewoner gbaBewoner, long plId)>? MogelijkeBewonersPlIds { get; set; }
	}
}
