namespace D2W.WebPortal.Models.Settings;

public class FileStorageSettings
{
    #region Public Properties

    public Guid Id { get; set; }
    public StorageTypes StorageType { get; set; }
    public Guid? TenantId { get; set; }

    #endregion Public Properties
}