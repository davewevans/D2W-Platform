namespace D2W.Application.Features.AppSettings.Queries.GetSettings.GetIdentitySettings;

public class IdentitySettingsForEdit
{
    #region Public Constructors

    public IdentitySettingsForEdit()
    {
        UserSettings = new UserSettings();
        PasswordSettings = new PasswordSettings();
        LockoutSettings = new LockoutSettings();
        SignInSettings = new SignInSettings();
    }

    #endregion Public Constructors

    #region Public Properties

    public UserSettings UserSettings { get; set; }
    public PasswordSettings PasswordSettings { get; set; }
    public LockoutSettings LockoutSettings { get; set; }
    public SignInSettings SignInSettings { get; set; }

    #endregion Public Properties
}