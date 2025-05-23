using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class GbaNaamBasis
	{
		/// <summary>
		/// De verzameling namen voor de geslachtsnaam, gescheiden door spaties.
		/// </summary>
		/// <value>De verzameling namen voor de geslachtsnaam, gescheiden door spaties. </value>
		[RegularExpression(@"^[a-zA-Z0-9À-ž \.\-\']{1,200}$")]
		[MaxLength(200)]
		[DataMember(Name = "voornamen", EmitDefaultValue = false)]
		public string? Voornamen { get; set; }

		/// <summary>
		/// Gets or Sets AdellijkeTitelPredicaat
		/// </summary>
		[DataMember(Name = "adellijkeTitelPredicaat", EmitDefaultValue = false)]
		public AdellijkeTitelPredicaatType? AdellijkeTitelPredicaat { get; set; }

		/// <summary>
		/// Gets or Sets Voorvoegsel
		/// </summary>
		[RegularExpression(@"^[a-zA-Z \']{1,10}$")]
		[MaxLength(10)]
		[DataMember(Name = "voorvoegsel", EmitDefaultValue = false)]
		public string? Voorvoegsel { get; set; }

		/// <summary>
		/// De achternaam van een persoon.
		/// </summary>
		/// <value>De achternaam van een persoon. </value>
		[RegularExpression(@"^[a-zA-Z0-9À-ž \.\-\']{1,200}$")]
		[DataMember(Name = "geslachtsnaam", EmitDefaultValue = false)]
		public string? Geslachtsnaam { get; set; }
	}
}
