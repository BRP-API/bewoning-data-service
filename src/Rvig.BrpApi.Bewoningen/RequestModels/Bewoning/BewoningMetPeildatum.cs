using System.Runtime.Serialization;

namespace Rvig.BrpApi.Bewoningen.RequestModels.Bewoning
{
	[DataContract]
	public class BewoningMetPeildatum : BewoningenQuery
	{
		/// <summary>
		/// Peildatum: de datum waarop je de bewoning wil weten.
		/// </summary>
		/// <value>Peildatum: de datum waarop je de bewoning wil weten. </value>
		[DataMember(Name = "peildatum", EmitDefaultValue = false)]
		public string? peildatum { get; set; }

		/// <summary>
		/// De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn.
		/// </summary>
		/// <value>De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn. </value>
		[DataMember(Name = "adresseerbaarObjectIdentificatie", EmitDefaultValue = false)]
		public string? adresseerbaarObjectIdentificatie { get; set; }
	}
}
