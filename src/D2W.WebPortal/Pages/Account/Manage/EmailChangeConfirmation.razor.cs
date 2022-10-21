namespace D2W.WebPortal.Pages.Account.Manage;

public partial class EmailChangeConfirmation
{
    #region Public Properties

    [Parameter] public bool DisplayConfirmAccountLink { get; set; }
    [Parameter] public string EmailConfirmationUrl { get; set; }

    #endregion Public Properties
}