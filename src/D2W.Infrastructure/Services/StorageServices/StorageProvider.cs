namespace D2W.Infrastructure.Services.StorageServices;

public class StorageProvider : IStorageProvider
{
    #region Private Fields

    private readonly IStorageFactory _storageFactory;
    private readonly IAppSettingsUseCase _appSettingsUseCase;

    #endregion Private Fields

    #region Public Constructors

    public StorageProvider(IStorageFactory storageFactory, IAppSettingsUseCase appSettingsUseCase)
    {
        _storageFactory = storageFactory;
        _appSettingsUseCase = appSettingsUseCase;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<IFileStorageService> InvokeInstanceAsync()
    {
        var storageTypeResponse = await _appSettingsUseCase.GetFileStorageSettings();
        var storageType = storageTypeResponse.Payload.StorageType;
        return _storageFactory.CreateInstance((StorageTypes)storageType);
    }

    #endregion Public Methods
}