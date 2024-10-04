using Brp.Shared.Infrastructure.Validatie;
using Brp.Shared.Infrastructure.Validatie.Validators;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;

namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie.Reisdocumenten;

public class RequestBodyValidatieService : IRequestBodyValidator
{
    public ValidationResult ValidateRequestBody(string requestBody)
    {
        var input = JObject.Parse(requestBody);
        return input.Value<string>("type") switch
        {
            "ZoekMetBurgerservicenummer" => new ZoekMetBurgerservicenummerQueryValidator().Validate(input),
            "RaadpleegMetReisdocumentnummer" => new RaadpleegMetReisdocumentnummerQueryValidator().Validate(input),
            _ => new RequestBodyValidator(new string[] {
                "ZoekMetBurgerservicenummer",
                "RaadpleegMetReisdocumentnummer"
            }).Validate(input),
        };
    }
}
