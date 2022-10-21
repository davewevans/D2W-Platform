namespace D2W.WebPortal.Models.Settings.IdentitySettings;

public class LockoutSettings
{
    #region Public Properties

    public Guid Id { get; set; }
    public bool AllowedForNewUsers { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
    public int DefaultLockoutTimeSpan { get; set; }

    #endregion Public Properties
}