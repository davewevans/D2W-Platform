namespace D2W.WebPortal.Features.Identity.Manage.Queries.Get2faState;

public class Get2FaStateResponse
{
    #region Public Properties

    public bool HasAuthenticator { get; set; }
    public int RecoveryCodesLeft { get; set; }
    public bool Is2FaEnabled { get; set; }
    public bool IsMachineRemembered { get; set; }

    #endregion Public Properties
}