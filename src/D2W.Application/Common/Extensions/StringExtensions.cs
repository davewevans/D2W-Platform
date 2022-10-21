namespace D2W.Application.Common.Extensions;

public static class StringExtensions
{
    #region Public Methods

    public static string Filter(this string str, List<char> charsToRemove)
    {
        return charsToRemove.Aggregate(str, (current, c) => current.Replace(c.ToString(), string.Empty));
    }

    public static string ReplaceSpaceAndSpecialCharsWithDashes(this string str)
    {
        var cleanedStr = Regex.Replace(str.Replace("@", "-"), "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled).Replace(" ", "");
        return cleanedStr;
    }

    #endregion Public Methods
}