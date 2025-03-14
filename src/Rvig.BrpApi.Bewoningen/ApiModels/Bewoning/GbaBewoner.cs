using Rvig.BrpApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Rvig.BrpApi.Shared.ApiModels.PersonenHistorieBase;

namespace Rvig.BrpApi.Bewoningen.ApiModels.Bewoning
{
	[DataContract]
    public sealed class GbaBewoner : IEquatable<GbaBewoner>
	{
        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }
        
        /// <summary>
        /// Gets or Sets Geboorte
        /// </summary>
        [DataMember(Name = "geboorte", EmitDefaultValue = false)]
        public GbaGeboorteBeperkt? Geboorte { get; set; }
        
        /// <summary>
        /// Gets or Sets Naam
        /// </summary>
        [DataMember(Name = "naam", EmitDefaultValue = false)]
        public GbaNaamBasis? Naam { get; set; }
        
        /// <summary>
        /// Gets or Sets GeheimhoudingPersoonsgegevens
        /// </summary>
        [Range(0, 7)]
        [DataMember(Name = "geheimhoudingPersoonsgegevens", EmitDefaultValue = false)]
        public int? GeheimhoudingPersoonsgegevens { get; set; }

        /// <summary>
        /// Gets or Sets VerblijfplaatsInOnderzoek
        /// </summary>
        [RegularExpression("^[0-9]{6}$")]
        [DataMember(Name = "verblijfplaatsInOnderzoek", EmitDefaultValue = false)]
        public GbaInOnderzoek? VerblijfplaatsInOnderzoek { get; set; }

        /// <summary>
        /// Gets or Sets Geslacht
        /// </summary>
        [DataMember(Name = "geslacht", EmitDefaultValue = false)]
        public Waardetabel? Geslacht { get; set; }

        public bool Equals(GbaBewoner? obj)
		{
			if (obj == null)
			{
				return false;
			}
			return Burgerservicenummer == obj.Burgerservicenummer
				 && GeheimhoudingPersoonsgegevens == obj.GeheimhoudingPersoonsgegevens;
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as GbaBewoner);
		}

		public override int GetHashCode()
		{
			return (Burgerservicenummer + "^" + GeheimhoudingPersoonsgegevens.ToString()+ "^" + VerblijfplaatsInOnderzoek?.ToString()).GetHashCode();
		}
	}
}
