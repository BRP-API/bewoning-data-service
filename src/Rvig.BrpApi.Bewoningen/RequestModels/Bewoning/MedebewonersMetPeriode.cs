using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.BrpApi.Shared.Validation;

namespace Rvig.BrpApi.Bewoningen.RequestModels.Bewoning
{
	[DataContract]
	public class MedebewonersMetPeriode : BewoningenQuery
	{
		/// <summary>
		/// De begindatum van de periode waarover de bewoning wordt opgevraagd.
		/// </summary>
		/// <value>De begindatum van de periode waarover de bewoning wordt opgevraagd. </value>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[ValidateDate("yyyy-MM-dd", ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.DateParse))]
		[DataMember(Name = "datumVan", EmitDefaultValue = false)]
		public string? datumVan { get; set; }

		/// <summary>
		/// De einddatum van de periode waarover de bewoning wordt opgevraagd.
		/// </summary>
		/// <value>De einddatum van de periode waarover de bewoning wordt opgevraagd. </value>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[DataMember(Name = "datumTot", EmitDefaultValue = false)]
		public string? datumTot { get; set; }

		/// <summary>
		/// Gets or Sets Burgerservicenummer
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[RegularExpression("^[0-9]{9}$", ErrorMessage = "Waarde voldoet niet aan patroon ^[0-9]{9}$.")]
		[DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
		public string? burgerservicenummer { get; set; }
	}
}
