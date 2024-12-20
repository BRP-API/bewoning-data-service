using System.Runtime.Serialization;

namespace Rvig.BrpApi.Bewoningen.ApiModels.Bewoning
{
	[DataContract]
    public class Periode
    {
        /// <summary>
        /// Gets or Sets DatumVan
        /// </summary>
        [DataMember(Name = "datumVan", EmitDefaultValue = false)]
        public string? DatumVan { get; set; }

        /// <summary>
        /// Gets or Sets DatumTot
        /// </summary>
        [DataMember(Name = "datumTot", EmitDefaultValue = false)]
        public string? DatumTot { get; set; }
    }
}
