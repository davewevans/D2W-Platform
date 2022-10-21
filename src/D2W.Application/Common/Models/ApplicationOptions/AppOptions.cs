namespace D2W.Application.Common.Models.ApplicationOptions;

public class AppOptions
{
    #region Public Fields

    public const string Section = "AppOptions";

    #endregion Public Fields

    #region Public Properties

    public AppIdentityOptions AppIdentityOptions { get; set; }
    public AppTokenOptions AppTokenOptions { get; set; }
    public AppFileStorageOptions AppFileStorageOptions { get; set; }

    #endregion Public Properties
}