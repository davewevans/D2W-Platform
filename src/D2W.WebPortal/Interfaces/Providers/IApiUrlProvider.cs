namespace D2W.WebPortal.Interfaces.Providers;

public interface IApiUrlProvider
{
    #region Public Properties

    string BaseUrl { get; }
    string BaseHubUrl { get; }

    #endregion Public Properties
}