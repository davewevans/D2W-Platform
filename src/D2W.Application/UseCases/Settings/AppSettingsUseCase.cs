namespace D2W.Application.UseCases.Settings;

public class AppSettingsUseCase : IAppSettingsUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IConfigReaderService _configReaderService;

    #endregion Private Fields

    #region Public Constructors

    public AppSettingsUseCase(IApplicationDbContext applicationDbContext,
                              IConfigReaderService configReaderService)
    {
        _applicationDbContext = applicationDbContext;
        _configReaderService = configReaderService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<IdentitySettingsForEdit>> GetIdentitySettings()
    {
        var userSettings = await GetUserSettings();
        var passwordSettings = await GetPasswordSettings();
        var lockoutSettings = await GetLockoutSettings();
        var signInSettings = await GeSignInSettings();

        var payload = new IdentitySettingsForEdit
        {
            UserSettings = userSettings,
            PasswordSettings = passwordSettings,
            LockoutSettings = lockoutSettings,
            SignInSettings = signInSettings,
        };
        return Envelope<IdentitySettingsForEdit>.Result.Ok(payload);
    }

    public async Task<Envelope<IdentitySettingsResponse>> EditIdentitySettings(UpdateIdentitySettingsCommand request)
    {
        var userSettings = await GetUserSettingsForEdit(request);

        var passwordSettings = await GetPasswordSettingsForEdit(request);

        var lockoutSettings = await GetLockoutSettingsForEdit(request);

        var signInSettings = await GetSignInSettingsForEdit(request);

        request.MapToEntity(userSettings, passwordSettings, lockoutSettings, signInSettings);

        _applicationDbContext.UserSettings.Update(userSettings);
        _applicationDbContext.PasswordSettings.Update(passwordSettings);
        _applicationDbContext.LockoutSettings.Update(lockoutSettings);
        _applicationDbContext.SignInSettings.Update(signInSettings);

        await _applicationDbContext.SaveChangesAsync();

        var identitySettingsResponse = new IdentitySettingsResponse
        {
            LockoutSettingsId = lockoutSettings.Id,
            PasswordSettingsId = passwordSettings.Id,
            SignInSettingsId = signInSettings.Id,
            UserSettingsId = userSettings.Id,
            SuccessMessage = Resource.Identity_settings_have_been_updated_successfully
        };

        return Envelope<IdentitySettingsResponse>.Result.Ok(identitySettingsResponse);
    }

    public async Task<Envelope<TokenSettingsResponse>> EditFileStorageSettings(UpdateFileStorageSettingsCommand request)
    {
        var fileStorageSettings = await _applicationDbContext.FileStorageSettings.FirstOrDefaultAsync(fs => fs.Id == request.Id)
                                  ?? _configReaderService.GetAppFileStorageOptions().MapToEntity();

        request.MapToEntity(fileStorageSettings);

        _applicationDbContext.FileStorageSettings.Update(fileStorageSettings);

        await _applicationDbContext.SaveChangesAsync();

        var tokenSettingsResponse = new TokenSettingsResponse
        {
            Id = fileStorageSettings.Id,
            SuccessMessage = Resource.File_storage_settings_have_been_updated_successfully
        };
        return Envelope<TokenSettingsResponse>.Result.Ok(tokenSettingsResponse);
    }

    public async Task<Envelope<FileStorageSettingsForEdit>> GetFileStorageSettings()
    {
        var fileStorageSettings = await _applicationDbContext.FileStorageSettings.FirstOrDefaultAsync()
                                  ?? _configReaderService.GetAppFileStorageOptions().MapToEntity();
        var payload = new FileStorageSettingsForEdit
        {
            Id = fileStorageSettings.Id,
            StorageType = (int)fileStorageSettings.StorageType
        };

        return Envelope<FileStorageSettingsForEdit>.Result.Ok(payload);
    }

    public async Task<Envelope<TokenSettingsResponse>> EditTokenSettings(UpdateTokenSettingsCommand request)
    {
        var tokenSettings = await _applicationDbContext.TokenSettings.FirstOrDefaultAsync(fs => fs.Id == request.TokenSettings.Id)
                            ?? _configReaderService.GetAppTokenOptions().MapToEntity();

        request.MapToEntity(tokenSettings);

        _applicationDbContext.TokenSettings.Update(tokenSettings);

        await _applicationDbContext.SaveChangesAsync();

        var tokenSettingsResponse = new TokenSettingsResponse
        {
            Id = tokenSettings.Id,
            SuccessMessage = Resource.Token_settings_have_been_updated_successfully
        };
        return Envelope<TokenSettingsResponse>.Result.Ok(tokenSettingsResponse);
    }

    public async Task<Envelope<TokenSettingsForEdit>> GetTokenSettings()
    {
        var tokenSettings = await _applicationDbContext.TokenSettings.FirstOrDefaultAsync()
                            ?? _configReaderService.GetAppTokenOptions().MapToEntity();

        var payload = new TokenSettingsForEdit
        {
            TokenSettings = tokenSettings
        };
        return Envelope<TokenSettingsForEdit>.Result.Ok(payload);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<SignInSettings> GeSignInSettings()
    {
        return await _applicationDbContext.SignInSettings.FirstOrDefaultAsync() ?? _configReaderService.GetAppSignInOptions().MapToEntity();
    }

    private async Task<LockoutSettings> GetLockoutSettings()
    {
        return await _applicationDbContext.LockoutSettings.FirstOrDefaultAsync() ?? _configReaderService.GetAppLockoutOptions().MapToEntity();
    }

    private async Task<PasswordSettings> GetPasswordSettings()
    {
        return await _applicationDbContext.PasswordSettings.FirstOrDefaultAsync() ?? _configReaderService.GetAppPasswordOptions().MapToEntity();
    }

    private async Task<UserSettings> GetUserSettings()
    {
        return await _applicationDbContext.UserSettings.FirstOrDefaultAsync() ?? _configReaderService.GetAppUserOptions().MapToEntity();
    }

    private async Task<SignInSettings> GetSignInSettingsForEdit(UpdateIdentitySettingsCommand request)
    {
        return await _applicationDbContext.SignInSettings.FirstOrDefaultAsync(ss => ss.Id == request.SignInSettings.Id)
               ?? _configReaderService.GetAppSignInOptions().MapToEntity();
    }

    private async Task<LockoutSettings> GetLockoutSettingsForEdit(UpdateIdentitySettingsCommand request)
    {
        return await _applicationDbContext.LockoutSettings.FirstOrDefaultAsync(ls => ls.Id == request.LockoutSettings.Id)
               ?? _configReaderService.GetAppLockoutOptions().MapToEntity();
    }

    private async Task<PasswordSettings> GetPasswordSettingsForEdit(UpdateIdentitySettingsCommand request)
    {
        return await _applicationDbContext.PasswordSettings.FirstOrDefaultAsync(ps => ps.Id == request.PasswordSettings.Id)
               ?? _configReaderService.GetAppPasswordOptions().MapToEntity();
    }

    private async Task<UserSettings> GetUserSettingsForEdit(UpdateIdentitySettingsCommand request)
    {
        return await _applicationDbContext.UserSettings.FirstOrDefaultAsync(us => us.Id == request.UserSettings.Id)
               ?? _configReaderService.GetAppUserOptions().MapToEntity();
    }

    #endregion Private Methods
}