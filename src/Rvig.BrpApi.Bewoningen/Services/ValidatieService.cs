using Rvig.BrpApi.Bewoningen.RequestModels.Bewoning;
using Rvig.BrpApi.Bewoningen.Validation.RequestModelValidators;
using Rvig.BrpApi.Bewoningen.ApiModels.Universal;
using Rvig.BrpApi.Bewoningen.Exceptions;

namespace Rvig.BrpApi.Bewoningen.Services
{
    public interface IValidatieService
    {
        void ValideerModel(BewoningenQuery model);
    }

    public class ValidatieService : IValidatieService
    {
        public void ValideerModel(BewoningenQuery model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            switch (model)
            {
                case (BewoningMetPeriode bewoningMetPeriode):
                    ValideerBewoningMetPeriode(bewoningMetPeriode);
                    break;
                case (BewoningMetPeildatum bewoningMetPeildatum):
                    ValideerBewoningMetPeildatum(bewoningMetPeildatum);
                    break;

                default:
                    throw new ArgumentException("Onbekend type query");
            }
        }

        private static void ValideerBewoningMetPeildatum(BewoningMetPeildatum bewoningMetPeildatum)
        {
            var peildatumValidator = new BewoningMetPeildatumValidator();
            var peildatumValidationResult = peildatumValidator.Validate(bewoningMetPeildatum);

            if (!peildatumValidationResult.IsValid)
            {
                throw new InvalidParamsException(peildatumValidationResult.Errors.Select(e => new InvalidParams { Code = "validation", Name = e.PropertyName, Reason = e.ErrorMessage }).ToList());
            }

        }

        private static void ValideerBewoningMetPeriode(BewoningMetPeriode bewoningMetPeriode)
        {
            var periodeValidator = new BewoningMetPeriodeValidator();
            var periodeValidationResult = periodeValidator.Validate(bewoningMetPeriode);

            if (!periodeValidationResult.IsValid)
            {
                throw new InvalidParamsException(periodeValidationResult.Errors.Select(e => new InvalidParams { Code = "validation", Name = e.PropertyName, Reason = e.ErrorMessage }).ToList());
            }
        }
    }
}
