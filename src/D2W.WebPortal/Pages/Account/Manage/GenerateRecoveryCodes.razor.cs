namespace D2W.WebPortal.Pages.Account.Manage;

public partial class GenerateRecoveryCodes
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IAppStateManager AppStateManager { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private string StatusMessage { get; set; }
    private bool IsTwoFactorEnabled { get; set; } = true;

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.CheckUser2FaState();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<User2FaStateResponse>;
            if (successResult.Result.IsTwoFactorEnabled == false)
            {
                IsTwoFactorEnabled = false;
                StatusMessage = successResult.Result.StatusMessage;
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

    private async Task Generate2FaRecoveryCodes()
    {
        var httpResponseWrapper = await ManageClient.GenerateRecoveryCodes();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<GenerateRecoveryCodesResponse>;
            if (successResult.Result.RecoveryCodes?.Any() != true)
                NavigationManager.NavigateTo($"account/manage/twoFactorAuthentication");

            AppStateManager.Value = successResult.Result.RecoveryCodes;

            MudDialog.Close(DialogResult.Ok("success"));
            Snackbar.Add(successResult.Result.StatusMessage, Severity.Success);

            ShowRecoveryCodes();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
    }

    private void ShowRecoveryCodes()
    {
        MudDialog.Close(DialogResult.Ok("success"));
        DialogService.Show<ShowRecoveryCodes>();
    }

    private void ResetAuthenticator()
    {
        DialogService.Show<ResetAuthenticator>();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    #endregion Private Methods
}