namespace D2W.Domain.Entities.Settings.IdentitySettings;

public class LockoutSettings : ISettingsSchema, IMayHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public bool AllowedForNewUsers { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
    public int DefaultLockoutTimeSpan { get; set; }
    public Guid? TenantId { get; set; }
    public bool IgnoreTenantId { get; set; }

    #endregion Public Properties
}