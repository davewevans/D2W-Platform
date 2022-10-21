namespace D2W.WebPortal.Pages.Account.Manage;

public partial class ShowRecoveryCodes
{
    #region Private Properties

    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IAppStateManager AppStateManager { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private string[] RecoveryCode { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.GetUser();

        if (httpResponseWrapper.Success)
        {
            if (AppStateManager.Value != null)
            {
                RecoveryCode = await Task.FromResult(((IEnumerable<string>)AppStateManager.Value).ToArray());
            }
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private void Close()
    {
        MudDialog.Close();
    }

    #endregion Private Methods
}