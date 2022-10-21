namespace D2W.WebPortal.Interfaces.Providers;

public interface IReturnUrlProvider
{
    #region Public Methods

    Task Clear();

    Task<string> GetReturnUrl();

    Task SetReturnUrl(string returnUrl);

    #endregion Public Methods
}