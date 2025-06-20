using System.ComponentModel.DataAnnotations;

namespace Bewoning.Api.Util
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RequiredNoEmptyListsAttribute : RequiredAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is IEnumerable<object> collection && !collection.Any())
            {
                return false;
            }

            return base.IsValid(value);
        }
    }
}