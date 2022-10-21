namespace D2W.WebPortal.Extensions;

public static class NavigationManagerExtensions
{
    #region Public Methods

    public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
    {
        var uri = navManager.ToAbsoluteUri(navManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
        {
            if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                value = (T)(object)valueFromQueryString.ToString();
                return true;
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
            {
                value = (T)(object)valueAsDecimal;
                return true;
            }
        }

        value = default;
        return false;
    }

    public static string GetSubDomain(this NavigationManager navManager)
    {
        var dot = '.';

        var navManagerBaseUri = navManager.BaseUri;

        var dotCount = navManagerBaseUri.Count(c => c == dot);

        if (dotCount == 0 && navManagerBaseUri.Contains("localhost"))
            return null; 
        
        if (dotCount == 1 && !navManagerBaseUri.Contains("localhost"))
            return null; 
        
        if (dotCount == 2 && navManagerBaseUri.Contains("www"))
            return null;

        var subDomain = navManagerBaseUri.Split('.')[0].Split("//")[1];
        return subDomain;
    }

    #endregion Public Methods
}