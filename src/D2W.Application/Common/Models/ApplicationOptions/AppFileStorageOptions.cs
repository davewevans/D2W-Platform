namespace D2W.Application.Common.Models.ApplicationOptions;

public class AppFileStorageOptions
{
    #region Public Fields

    public const string Section = "FileStorageOptions";

    #endregion Public Fields

    #region Public Properties

    public Guid Id { get; set; }
    public int StorageType { get; set; }

    #endregion Public Properties

    #region Public Methods

    public FileStorageSettings MapToEntity()
    {
        return new()
        {
            StorageType = (StorageTypes)Enum.Parse(typeof(StorageTypes), StorageType.ToString(), true),
        };
    }

    #endregion Public Methods
}