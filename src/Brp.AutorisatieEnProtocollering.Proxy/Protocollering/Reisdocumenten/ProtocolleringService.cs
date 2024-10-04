using Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Reisdocumenten;
using Brp.AutorisatieEnProtocollering.Proxy.Helpers;
using Newtonsoft.Json.Linq;

namespace Brp.AutorisatieEnProtocollering.Proxy.Protocollering.Reisdocumenten
{
    public class ProtocolleringService : AbstractProtocolleringService
    {
        public ProtocolleringService(IServiceProvider serviceProvider)
            : base(serviceProvider, Constanten.FieldElementNrDictionary)
        {
        }

        private static string BepaalKeyVoor(string gevraagdField, string zoekType) => gevraagdField;

        protected override IEnumerable<(string Name, string[] Value)> BepaalElementNrVanFieldsVoorProtocollering(JObject input)
        {
            var zoekType = input.WaardeTypeParameter();
            var gevraagdeFields = input.WaardeFieldsParameter();

            return gevraagdeFields.ToKeyStringArray(Constanten.FieldElementNrDictionary, zoekType!, BepaalKeyVoor);
        }
    }
}
