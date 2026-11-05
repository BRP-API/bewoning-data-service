using System.Text.RegularExpressions;

namespace Brp.Shared.DtoMappers;

public static class StringExtensions
{
    public static string RemoveRedundantSpaces(this string input)
    {
        var retval = Regex.Replace(input, @"\s+", " ", RegexOptions.None, TimeSpan.FromMilliseconds(100));
        retval = Regex.Replace(retval, @"\-\s+", "-", RegexOptions.None, TimeSpan.FromMilliseconds(100));

        return retval.Trim();
    }
}
