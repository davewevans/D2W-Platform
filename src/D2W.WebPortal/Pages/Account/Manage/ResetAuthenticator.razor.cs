namespace D2W.WebPortal.Pages.Account.Manage;

public partial class ResetAuthenticator
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.GetUser();

        if (!httpResponseWrapper.Success)
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task ResetAuthenticatorApp()
    {
        var httpResponseWrapper = await ManageClient.ResetAuthenticator();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ResetAuthenticatorResponse>;
            await AuthenticationService.ReAuthenticate(successResult.Result.AuthResponse);
            MudDialog.Close(DialogResult.Ok("success"));
            Snackbar.Add(successResult.Result.StatusMessage, Severity.Success);
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    #endregion Private Methods
}