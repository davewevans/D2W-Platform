namespace D2W.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppLockoutOptions
{
    #region Public Fields

    public const string Section = "AppLockoutOptions";

    #endregion Public Fields

    #region Public Properties

    public bool AllowedForNewUsers { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
    public int DefaultLockoutTimeSpan { get; set; }

    #endregion Public Properties

    #region Public Methods

    public LockoutSettings MapToEntity()
    {
        return new()
        {
            AllowedForNewUsers = AllowedForNewUsers,
            MaxFailedAccessAttempts = MaxFailedAccessAttempts,
            DefaultLockoutTimeSpan = DefaultLockoutTimeSpan
        };
    }

    #endregion Public Methods
}