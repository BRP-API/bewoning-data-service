using Bewoning.Api.Fields;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.Interfaces;
using Bewoning.Api.Validation.RequestModelValidators;
using Bewoning.Api.ResponseModels.Bewoning;

namespace Bewoning.Api.Services
{
    public interface IFilterService
    {
        void FilterResponse(BewoningenQuery model, GbaBewoningenQueryResponse response);
    }

    public class FilterService : BaseApiService, IFilterService
    {
        protected override FieldsSettings _fieldsSettings => throw new NotImplementedException();

        public FilterService(IDomeinTabellenRepo domeinTabellenRepo) : base(domeinTabellenRepo)
        {
        }

        public void FilterResponse(BewoningenQuery model, GbaBewoningenQueryResponse response)
        {
            switch (model)
            {
                case BewoningMetPeildatum peildatumModel:
                    FilterResponseByPeildatum(peildatumModel, response);
                    break;
                case BewoningMetPeriode periodeModel:
                    FilterResponseByPeriode(periodeModel, response);
                    break;
                default:
                    throw new ArgumentException("Onbekend type query");
            }
        }

        private static void FilterResponseByPeriode(BewoningMetPeriode model, GbaBewoningenQueryResponse bewoningenResponse)
        {
            bewoningenResponse.Bewoningen = FilterByDatumVanDatumTot(
                DatumValidator.ValidateAndParseDate(model.datumVan, nameof(model.datumVan)),
                DatumValidator.ValidateAndParseDate(model.datumTot, nameof(model.datumTot)),
                bewoningenResponse.Bewoningen!,
                $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}",
                $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
        }

        private static void FilterResponseByPeildatum(BewoningMetPeildatum model, GbaBewoningenQueryResponse bewoningenResponse)
        {
            bewoningenResponse.Bewoningen = FilterByPeildatumAndFields(
                DatumValidator.ValidateAndParseDate(model.peildatum, nameof(model.peildatum)),
                bewoningenResponse.Bewoningen!,
                $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumVan)}",
                $"{nameof(GbaBewoning.Periode)}.{nameof(Periode.DatumTot)}");
        }
    }
}
