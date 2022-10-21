namespace D2W.Application.Common.Interfaces.UseCases.Settings;

public interface IAppSettingsUseCase
{
    #region Public Methods

    Task<Envelope<IdentitySettingsForEdit>> GetIdentitySettings();

    Task<Envelope<IdentitySettingsResponse>> EditIdentitySettings(UpdateIdentitySettingsCommand request);

    Task<Envelope<FileStorageSettingsForEdit>> GetFileStorageSettings();

    Task<Envelope<TokenSettingsResponse>> EditFileStorageSettings(UpdateFileStorageSettingsCommand request);

    Task<Envelope<TokenSettingsResponse>> EditTokenSettings(UpdateTokenSettingsCommand request);

    Task<Envelope<TokenSettingsForEdit>> GetTokenSettings();

    #endregion Public Methods
}