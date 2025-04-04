using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.BrpApi.Shared.ApiModels.Universal
{
    /// <summary>
    /// Details over fouten in opgegeven parameters
    /// </summary>
    [DataContract]
    public class InvalidParams
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? Type { get; set; }

        /// <summary>
        /// Naam van de parameter
        /// </summary>
        /// <value>Naam van de parameter</value>
        [RegularExpression(@"^[a-zA-Z0-9\.,_]{1,30}$")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string? Name { get; set; }

        /// <summary>
        /// Systeemcode die het type fout aangeeft
        /// </summary>
        /// <value>Systeemcode die het type fout aangeeft</value>
        [RegularExpression(@"^[a-zA-Z0-9\.,_]{1,25}$")]
        [MinLength(1)]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string? Code { get; set; }

        /// <summary>
        /// Beschrijving van de fout op de parameterwaarde
        /// </summary>
        /// <value>Beschrijving van de fout op de parameterwaarde</value>
        [RegularExpression(@"^[a-zA-Z0-9\.,_ ]{1,80}$")]
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string? Reason { get; set; }
    }
}
