using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.BrpApi.Shared.ApiModels.Universal
{
    /// <summary>
    /// Terugmelding bij een fout. JSON representatie in lijn met [RFC7807](https://tools.ietf.org/html/rfc7807).
    /// </summary>
    [DataContract]
    public class Foutbericht
    {
        /// <summary>
        /// Link naar meer informatie over deze fout
        /// </summary>
        /// <value>Link naar meer informatie over deze fout</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? Type { get; set; }

        /// <summary>
        /// Beschrijving van de fout
        /// </summary>
        /// <value>Beschrijving van de fout</value>
        [RegularExpression(@"^[a-zA-Z0-9À-ž \.\-]{1,80}$")]
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string? Title { get; set; }

        /// <summary>
        /// Http status code
        /// </summary>
        /// <value>Http status code</value>
        [Range(100, 600)]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Details over de fout
        /// </summary>
        /// <value>Details over de fout</value>
        [RegularExpression(@"^[a-zA-Z0-9À-ž \.\-\(\)\,]{1,200}$")]
        [DataMember(Name = "detail", EmitDefaultValue = false)]
        public string? Detail { get; set; }

        /// <summary>
        /// Uri van de aanroep die de fout heeft veroorzaakt
        /// </summary>
        /// <value>Uri van de aanroep die de fout heeft veroorzaakt</value>
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string? Instance { get; set; }

        /// <summary>
        /// Systeemcode die het type fout aangeeft
        /// </summary>
        /// <value>Systeemcode die het type fout aangeeft</value>
        [RegularExpression("^[a-zA-Z0-9]{1,25}$")]
        [MinLength(1)]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string? Code { get; set; }
    }
}
