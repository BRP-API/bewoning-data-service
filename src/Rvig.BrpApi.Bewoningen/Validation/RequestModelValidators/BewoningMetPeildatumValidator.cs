using FluentValidation;
using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;

namespace Rvig.BrpApi.Bewoningen.Validation.RequestModelValidators;

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
            .Must((model, peildatum) => DatumValidator.ValidateAndParseDate(peildatum, "peildatum") != null)
			.Matches(_datePattern).WithMessage(_dateErrorMessage);
	}
}
