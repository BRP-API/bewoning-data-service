using Rvig.BrpApi.Bewoningen.ApiModels.Universal;
using Rvig.BrpApi.Bewoningen.Exceptions;
using System.Globalization;

namespace Rvig.BrpApi.Bewoningen.Validation.RequestModelValidators
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
