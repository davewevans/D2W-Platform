namespace D2W.Infrastructure.Services.StorageServices;

public class StorageFactory : IStorageFactory
{
    #region Private Fields

    private readonly IDictionary<StorageTypes, IFileStorageService> _factories;

    private readonly IEnumerable<IFileStorageService> _fileStorageServices;

    #endregion Private Fields

    #region Public Constructors

    public StorageFactory(IEnumerable<IFileStorageService> fileStorageServices)
    {
        _fileStorageServices = fileStorageServices;

        _factories = new Dictionary<StorageTypes, IFileStorageService>();

        foreach (StorageTypes storageType in Enum.GetValues(typeof(StorageTypes)))
        {
            _factories.Add(storageType, _fileStorageServices.FirstOrDefault(h => h.GetType().Name == storageType.ToString()));
        }
    }

    #endregion Public Constructors

    #region Public Methods

    public IFileStorageService CreateInstance(StorageTypes storageType)
    {
        return _factories[storageType];
    }

    #endregion Public Methods
}