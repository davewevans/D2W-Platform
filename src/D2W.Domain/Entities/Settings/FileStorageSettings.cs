namespace D2W.Domain.Entities.Settings;

public class FileStorageSettings : ISettingsSchema, IMayHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public StorageTypes StorageType { get; set; }
    public Guid? TenantId { get; set; }
    public bool IgnoreTenantId { get; set; }

    #endregion Public Properties
}