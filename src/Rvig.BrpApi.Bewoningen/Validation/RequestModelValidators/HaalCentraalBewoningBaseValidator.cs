using FluentValidation;
using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;
using Rvig.BrpApi.Shared.Validation.RequestModelValidators;

namespace Rvig.BrpApi.Bewoningen.Validation.RequestModelValidators;

public class HaalCentraalBewoningBaseValidator<T> : HaalCentraalBaseValidator<T> where T : BewoningenQuery
{
	public HaalCentraalBewoningBaseValidator()
	{
		RuleFor(x => x.type)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage);
	}

	protected const string _adresseerbaarObjectIdentificatiePattern = "^(?!0{16})[0-9]{16}$";
}
