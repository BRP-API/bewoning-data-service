using FluentValidation;
using Rvig.HaalCentraalApi.Bewoningen.RequestModels.Bewoning;

namespace Rvig.HaalCentraalApi.Bewoning.Validation.RequestModelValidators;

public class BewoningMetPeildatumValidator : HaalCentraalBewoningBaseValidator<BewoningMetPeildatum>
{
	public BewoningMetPeildatumValidator()
	{
		RuleFor(x => x.adresseerbaarObjectIdentificatie)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_adresseerbaarObjectIdentificatiePattern).WithMessage(GetPatternErrorMessage(_adresseerbaarObjectIdentificatiePattern));

		RuleFor(x => x.peildatum)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_datePattern).WithMessage(_dateErrorMessage);
	}
}
