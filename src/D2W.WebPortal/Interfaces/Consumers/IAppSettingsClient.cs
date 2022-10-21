namespace D2W.WebPortal.Interfaces.Consumers;

public interface IAppSettingsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetFileStorageSettings();

    Task<HttpResponseWrapper<object>> GetIdentitySettings();

    Task<HttpResponseWrapper<object>> GetTokenSettings();

    Task<HttpResponseWrapper<object>> UpdateFileStorageSettings(UpdateFileStorageSettingsCommand request);

    Task<HttpResponseWrapper<object>> UpdateIdentitySettings(UpdateIdentitySettingsCommand request);

    Task<HttpResponseWrapper<object>> UpdateTokenSettings(UpdateTokenSettingsCommand request);

    #endregion Public Methods
}