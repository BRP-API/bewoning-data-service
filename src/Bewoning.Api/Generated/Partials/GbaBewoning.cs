using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Bewoning.Api.Generated.Partials
{
    [DataContract]
    public partial class GbaBewoning : Generated.GbaBewoning
    {
        // Used for protocollering. DO NOT SERIALIZE AS PART OF THE API RESPONSE.
        [XmlIgnore, JsonIgnore]
        public List<(GbaBewoner gbaBewoner, long plId)>? BewonersPlIds { get; set; }

        [XmlIgnore, JsonIgnore]
        public List<(GbaBewoner gbaBewoner, long plId)>? MogelijkeBewonersPlIds { get; set; }
    }
}
