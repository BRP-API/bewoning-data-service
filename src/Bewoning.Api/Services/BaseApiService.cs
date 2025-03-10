using Bewoning.Api.Fields;
using Bewoning.Api.Interfaces;
using Bewoning.Api.Validation;

namespace Bewoning.Api.Services
{
    public abstract class BaseApiService
    {
        protected IDomeinTabellenRepo _domeinTabellenRepo;

        protected abstract FieldsSettings _fieldsSettings { get; }
        protected readonly FieldsFilterService _fieldsExpandFilterService = new();

        protected BaseApiService(IDomeinTabellenRepo domeinTabellenRepo)
        {
            _domeinTabellenRepo = domeinTabellenRepo;
        }

        protected static List<T>? FilterByPeildatum<T>(DateTime? peildatum, List<T> objectsToFilter, string? beginDatePropName, string? endDatePropName) where T : class
        {
            return objectsToFilter
                            ?.Where(x => ValidationHelperBase.IsPeildatumBetweenStartAndEndDates(peildatum, GetValue(x, beginDatePropName) as string, GetValue(x, endDatePropName) as string))
                            .ToList();
        }

        protected static List<T>? FilterByPeildatumAndFields<T>(DateTime? peildatum, List<T> objectsToFilter, string? beginDatePropName, string? endDatePropName) where T : class
        {
            return FilterByPeildatum(peildatum, objectsToFilter, beginDatePropName, endDatePropName);
        }

        public static List<T>? FilterByDatumVanDatumTot<T>(DateTime? datumVan, DateTime? datumTot, List<T> objectsToFilter, string? beginDatePropName, string? endDatePropName) where T : class
        {
            return objectsToFilter
                            ?.Where(x => ValidationHelperBase.TimePeriodesOverlap(datumVan, datumTot, GetValue(x, beginDatePropName) as string, GetValue(x, endDatePropName) as string))
                            .ToList();
        }

        protected List<T>? FilterByDatumVanDatumTotAndFields<T>(DateTime? datumVan, DateTime? datumTot, List<string> fields, List<T> objectsToFilter, FieldsSettingsModel fieldsModel, string? beginDatePropName, string? endDatePropName) where T : class
            => FilterByDatumVanDatumTot(datumVan, datumTot, objectsToFilter, beginDatePropName, endDatePropName)
                            ?.Select(x => _fieldsExpandFilterService.ApplyScope(x!, string.Join(",", fields), fieldsModel))
                            .ToList();

        /// <summary>
        /// Using reflection get the value of a property of an object using the property name.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        private static object? GetValue(object? sourceObject, string? propName)
        {
            var parentType = sourceObject?.GetType();
            if (string.IsNullOrWhiteSpace(propName))
            {
                return null;
            }
            var propertyParts = propName.Split('.');
            (object? prev, object? current, bool first) = (null, null, true);
            foreach (var propPart in propertyParts)
            {
                if (!first)
                {
                    parentType = prev?.GetType();
                    sourceObject = prev;
                }

                var prop = parentType?.GetProperty(propPart);
                if (prop == null)
                {
                    return null;
                }
                current = prop.GetValue(sourceObject);
                prev = current;
                first = false;
            }
            return current;
        }
    }
}