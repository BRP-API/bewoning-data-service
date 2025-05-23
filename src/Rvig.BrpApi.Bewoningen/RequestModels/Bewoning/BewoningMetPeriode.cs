using System.Runtime.Serialization;

namespace Rvig.BrpApi.Bewoningen.RequestModels.Bewoning
{
	[DataContract]
	public class BewoningMetPeriode : BewoningenQuery
	{
		/// <summary>
		/// De begindatum van de periode waarover de bewoning wordt opgevraagd.
		/// </summary>
		/// <value>De begindatum van de periode waarover de bewoning wordt opgevraagd. </value>
		[DataMember(Name = "datumVan", EmitDefaultValue = false)]
		public string? datumVan { get; set; }

		/// <summary>
		/// De einddatum van de periode waarover de bewoning wordt opgevraagd.
		/// </summary>
		/// <value>De einddatum van de periode waarover de bewoning wordt opgevraagd. </value>
		[DataMember(Name = "datumTot", EmitDefaultValue = false)]
		public string? datumTot { get; set; }

		/// <summary>
		/// De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn.
		/// </summary>
		/// <value>De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn. </value>
		[DataMember(Name = "adresseerbaarObjectIdentificatie", EmitDefaultValue = false)]
		public string? adresseerbaarObjectIdentificatie { get; set; }
	}
}
