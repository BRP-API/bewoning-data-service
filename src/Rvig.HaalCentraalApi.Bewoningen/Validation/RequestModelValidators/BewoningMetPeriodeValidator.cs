using FluentValidation;
using Rvig.HaalCentraalApi.Bewoning.Validation.RequestModelValidators;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;

namespace Rvig.HaalCentraalApi.Bewoningen.Validation.RequestModelValidators;

public class BewoningMetPeriodeValidator : HaalCentraalBewoningBaseValidator<BewoningMetPeriode>
{
	public BewoningMetPeriodeValidator()
	{
		RuleFor(x => x.adresseerbaarObjectIdentificatie)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_adresseerbaarObjectIdentificatiePattern).WithMessage(GetPatternErrorMessage(_adresseerbaarObjectIdentificatiePattern));

		RuleFor(x => x.datumVan)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_datePattern).WithMessage(_dateErrorMessage);

		RuleFor(x => x.datumTot)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_datePattern).WithMessage(_dateErrorMessage);
	}
}
