namespace D2W.WebPortal.Services;

public class NavigationService : INavigationService
{
    #region Private Fields

    private readonly IJSRuntime _jsRuntime;

    #endregion Private Fields

    #region Public Constructors

    public NavigationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task NavigateToUrlAsync(string url, bool openInNewTab)
    {
        if (openInNewTab)
        {
            await _jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }
        else
        {
            await _jsRuntime.InvokeVoidAsync("open", url);
        }
    }

    #endregion Public Methods
}