namespace D2W.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppSignInOptions
{
    #region Public Fields

    public const string Section = "AppSignInOptions";

    #endregion Public Fields

    #region Public Properties

    //public bool RequireConfirmedPhoneNumber { get; set; }
    //public bool RequireConfirmedEmail { get; set; }
    public bool RequireConfirmedAccount { get; set; }

    #endregion Public Properties

    #region Public Methods

    public SignInSettings MapToEntity()
    {
        return new()
        {
            //RequireConfirmedEmail = RequireConfirmedEmail,
            //RequireConfirmedPhoneNumber = RequireConfirmedPhoneNumber,
            RequireConfirmedAccount = RequireConfirmedAccount
        };
    }

    #endregion Public Methods
}