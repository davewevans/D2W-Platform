namespace D2W.HostApp.Providers;

public class TenantUrlProvider : ITenantUrlProvider
{
    #region Private Fields

    private readonly UrlOptions _optionsSnapshot;

    #endregion Private Fields

    #region Public Properties

    public TenantUrlProvider(IOptionsSnapshot<UrlOptions> optionsSnapshot)
    {
        _optionsSnapshot = optionsSnapshot.Value;
    }

    public string TenantUrl => _optionsSnapshot.TenantUrl;

    #endregion Public Properties
}