using Brp.Shared.Infrastructure.Validatie.Validators;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie.Reisdocumenten;

public class RaadpleegMetReisdocumentnummerQueryValidator : AbstractValidator<JObject>
{
    public RaadpleegMetReisdocumentnummerQueryValidator()
    {
        Include(new NietGespecificeerdeParametersValidator(GespecificeerdeParameterNamen));
        Include(new ReisdocumentnummerCollectieValidator(true));
        Include(new FieldsValidator(Constanten.ReisdocumentFields, Constanten.NotAllowedReisdocumentFields, 25));
    }

    private readonly List<string> GespecificeerdeParameterNamen = new()
    {
        "type",
        "reisdocumentnummer",
        "fields"
    };
}
