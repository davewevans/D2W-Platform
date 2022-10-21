namespace D2W.WebPortal.Pages.Account.Manage;

public partial class DisableTwoFactorAuthentication
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }

    #endregion Private Properties

    #region Private Methods

    private async Task DisableAuthenticatorApp()
    {
        var httpResponseWrapper = await ManageClient.Disable2Fa();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<string>;
            MudDialog.Close(DialogResult.Ok("success"));
            Snackbar.Add(successResult.Result, Severity.Success);
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
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