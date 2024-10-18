using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using System.Globalization;

namespace Rvig.HaalCentraalApi.Bewoningen.Validation.RequestModelValidators
{
    public static class DatumValidator
    {
        public static DateTime? ValidateAndParseDate(string? dateString, string fieldName)
        {
            if (DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateValue))
            {
                return dateValue;
            }
            throw new InvalidParamsException(new List<InvalidParams>
            {
                new() { Code = "date", Name = fieldName, Reason = "Waarde is geen geldige datum." }
            });
        }
    }
}
