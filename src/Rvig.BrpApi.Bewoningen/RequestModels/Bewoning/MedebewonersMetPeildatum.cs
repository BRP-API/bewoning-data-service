using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.BrpApi.Shared.Validation;

namespace Rvig.BrpApi.Bewoningen.RequestModels.Bewoning
{
	[DataContract]
	public class MedebewonersMetPeildatum : BewoningenQuery
	{
		/// <summary>
		/// Peildatum: de datum waarop je de bewoning wil weten.
		/// </summary>
		/// <value>Peildatum: de datum waarop je de bewoning wil weten. </value>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[ValidateDate("yyyy-MM-dd", ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.DateParse))]
		[DataMember(Name = "peildatum", EmitDefaultValue = false)]
		public string? peildatum { get; set; }

		/// <summary>
		/// Gets or Sets Burgerservicenummer
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[RegularExpression("^[0-9]{9}$", ErrorMessage = "Waarde voldoet niet aan patroon ^[0-9]{9}$.")]
		[DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
		public string? burgerservicenummer { get; set; }
	}
}
