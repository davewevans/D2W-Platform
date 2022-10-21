namespace D2W.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppIdentityOptions
{
    #region Public Fields

    public const string Section = "AppIdentityOptions";

    #endregion Public Fields

    #region Public Properties

    public AppUserOptions AppUserOptions { get; set; }
    public AppPasswordOptions AppPasswordOptions { get; set; }
    public AppLockoutOptions AppLockoutOptions { get; set; }
    public AppSignInOptions AppSignInOptions { get; set; }

    #endregion Public Properties
}