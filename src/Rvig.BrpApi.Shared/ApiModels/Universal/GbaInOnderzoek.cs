using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.BrpApi.Shared.Validation;

namespace Rvig.BrpApi.Shared.ApiModels.Universal
{
	[DataContract]
	public sealed class GbaInOnderzoek : IEquatable<GbaInOnderzoek>
	{
		/// <summary>
		/// Gets or Sets AanduidingGegevensInOnderzoek
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[RegularExpression("^[0-9]{6}$")]
		[DataMember(Name = "aanduidingGegevensInOnderzoek", EmitDefaultValue = false)]
		public string? AanduidingGegevensInOnderzoek { get; set; }

		/// <summary>
		/// Gets or Sets DatumIngangOnderzoek
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datumIngangOnderzoek", EmitDefaultValue = false)]
		public string? DatumIngangOnderzoek { get; set; }

		/// <summary>
		/// Gets or Sets DatumIngangOnderzoek
		/// </summary>
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datumEindeOnderzoek", EmitDefaultValue = false)]
		public string? DatumEindeOnderzoek { get; set; }

		public bool Equals(GbaInOnderzoek? obj)
		{
			if (obj == null)
			{
				return false;
			}
			return AanduidingGegevensInOnderzoek == obj.AanduidingGegevensInOnderzoek
				 && DatumIngangOnderzoek == obj.DatumIngangOnderzoek;
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as GbaInOnderzoek);
		}

		public override int GetHashCode()
		{
			return (AanduidingGegevensInOnderzoek + "^" + DatumIngangOnderzoek).GetHashCode();
		}
	}
}
