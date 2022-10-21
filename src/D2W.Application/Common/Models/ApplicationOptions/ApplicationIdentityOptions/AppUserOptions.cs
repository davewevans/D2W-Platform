namespace D2W.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppUserOptions
{
    #region Public Fields

    public const string Section = "AppUserOptions";

    #endregion Public Fields

    #region Public Properties

    public string AllowedUserNameCharacters { get; set; }
    public bool NewUsersActiveByDefault { get; set; }

    #endregion Public Properties

    #region Public Methods

    public UserSettings MapToEntity()
    {
        return new()
        {
            AllowedUserNameCharacters = AllowedUserNameCharacters,
            NewUsersActiveByDefault = NewUsersActiveByDefault
        };
    }

    #endregion Public Methods
}