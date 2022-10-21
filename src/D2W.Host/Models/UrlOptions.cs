namespace D2W.HostApp.Models;

public class UrlOptions
{
    #region Public Fields

    public const string Section = "UrlOptions";

    #endregion Public Fields

    #region Public Properties

    public string BaseApiUrl { get; set; }
    public string TenantUrl { get; set; }

    #endregion Public Properties
}