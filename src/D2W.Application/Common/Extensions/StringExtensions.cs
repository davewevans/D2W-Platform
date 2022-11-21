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

    public static (string, string) SplitFullName(this string fullName)
    {
        var nameSplit = fullName?.Split(' ');
        string firstName = string.Empty;
        string lastName = string.Empty;
        if (nameSplit != null)
        {
            firstName = nameSplit[0];
            lastName = nameSplit[^1];
        }

        return (firstName, lastName);
    }

    #endregion Public Methods
}