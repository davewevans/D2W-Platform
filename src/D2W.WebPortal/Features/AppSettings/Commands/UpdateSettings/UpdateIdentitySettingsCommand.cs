namespace D2W.WebPortal.Features.AppSettings.Commands.UpdateSettings;

public class UpdateIdentitySettingsCommand
{
    #region Public Properties

    public UserSettings UserSettings { get; set; }
    public PasswordSettings PasswordSettings { get; set; }
    public LockoutSettings LockoutSettings { get; set; }
    public SignInSettings SignInSettings { get; set; }

    #endregion Public Properties
}