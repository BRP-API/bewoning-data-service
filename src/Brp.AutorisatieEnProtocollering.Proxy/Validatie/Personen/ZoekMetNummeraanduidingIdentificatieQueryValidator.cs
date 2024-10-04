using Brp.Shared.Infrastructure.Validatie.Validators;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie.Personen;

public class ZoekMetNummeraanduidingIdentificatieQueryValidator : AbstractValidator<JObject>
{
    public ZoekMetNummeraanduidingIdentificatieQueryValidator()
    {
        Include(new NietGespecificeerdeParametersValidator(GespecificeerdeParameterNamen));
        Include(new NummeraanduidingIdentificatieVerplichtValidator());
        Include(new InclusiefOverledenPersonenOptioneelValidator());
        Include(new GemeenteVanInschrijvingValidator());
        Include(new FieldsValidator(Constanten.PersoonBeperktFields, Constanten.NotAllowedPersoonFields, 130));
    }

    private readonly List<string> GespecificeerdeParameterNamen = new()
    {
        "type",
        "gemeenteVanInschrijving",
        "nummeraanduidingIdentificatie",
        "inclusiefOverledenPersonen",
        "fields"
    };
}
