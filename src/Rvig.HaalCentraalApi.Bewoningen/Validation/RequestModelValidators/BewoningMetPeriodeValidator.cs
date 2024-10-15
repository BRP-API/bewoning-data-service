using FluentValidation;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;

namespace Rvig.HaalCentraalApi.Bewoningen.Validation.RequestModelValidators;

public class BewoningMetPeriodeValidator : HaalCentraalBewoningBaseValidator<BewoningMetPeriode>
{
	public BewoningMetPeriodeValidator()
	{
		RuleFor(x => x.adresseerbaarObjectIdentificatie)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_adresseerbaarObjectIdentificatiePattern)
            .WithMessage(GetPatternErrorMessage(_adresseerbaarObjectIdentificatiePattern));

		RuleFor(x => x.datumVan)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_datePattern)
			.Must((model, datumVan) => DatumValidator.ValidateAndParseDate(datumVan, "datumVan") != null)
			.WithMessage(_dateErrorMessage);

		RuleFor(x => x.datumTot)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_datePattern)
			.Must((model, datumTot) => DatumValidator.ValidateAndParseDate(datumTot, "datumTot") != null)
            .WithMessage(_dateErrorMessage);

        RuleFor(x => x)
            .Must(model => DatumValidator.ValidateAndParseDate(model.datumVan, "datumVan") < DatumValidator.ValidateAndParseDate(model.datumTot, "datumTot"))
            .WithMessage("datumTot moet na datumVan liggen.");
    }
}
