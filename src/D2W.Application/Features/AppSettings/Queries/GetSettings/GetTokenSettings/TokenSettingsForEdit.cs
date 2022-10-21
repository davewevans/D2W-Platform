namespace D2W.Application.Features.AppSettings.Queries.GetSettings.GetTokenSettings;

public class TokenSettingsForEdit
{
    #region Public Constructors

    public TokenSettingsForEdit()
    {
        TokenSettings = new TokenSettings();
    }

    #endregion Public Constructors

    #region Public Properties

    public TokenSettings TokenSettings { get; set; }

    #endregion Public Properties
}