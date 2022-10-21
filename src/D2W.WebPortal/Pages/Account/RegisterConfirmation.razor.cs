namespace D2W.WebPortal.Pages.Account;

public partial class RegisterConfirmation
{
    #region Public Properties

    [Parameter] public bool DisplayConfirmAccountLink { get; set; }
    [Parameter] public string EmailConfirmationUrl { get; set; }

    #endregion Public Properties
}