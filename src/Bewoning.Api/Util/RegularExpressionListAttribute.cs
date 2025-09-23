using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bewoning.Api.Util
{
    public class RegularExpressionListAttribute : RegularExpressionAttribute
    {
        public RegularExpressionListAttribute(string pattern)
            : base(pattern) { }

        public override bool IsValid(object? value)
        {
            return value is IEnumerable<string> valueList
                                                    && valueList.All(val => Regex.IsMatch(val, Pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100)));
        }
    }
}
