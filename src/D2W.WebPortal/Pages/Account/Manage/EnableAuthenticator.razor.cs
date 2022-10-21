namespace D2W.WebPortal.Pages.Account.Manage;

public partial class EnableAuthenticator
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private IAppStateManager AppStateManager { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private LoadSharedKeyAndQrCodeUriResponse LoadSharedKeyAndQrCodeUriResponse { get; set; } = new();
    private EnableAuthenticatorCommand EnableAuthenticatorCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.LoadSharedKeyAndQrCodeUri();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<LoadSharedKeyAndQrCodeUriResponse>;
            LoadSharedKeyAndQrCodeUriResponse = successResult.Result;
            await GenerateQrCode();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task GenerateQrCode()
    {
        await Js.InvokeVoidAsync("generateQrCode", LoadSharedKeyAndQrCodeUriResponse.AuthenticatorUri);
    }

    private async Task EnableAuthenticatorApp()
    {
        var httpResponseWrapper = await ManageClient.EnableAuthenticator(EnableAuthenticatorCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<EnableAuthenticatorResponse>;

            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);

            if (successResult.Result.ShowRecoveryCodes)
            {
                AppStateManager.Value = successResult.Result.RecoveryCodes;
                MudDialog.Close(DialogResult.Ok("success"));
                ShowRecoveryCodes();
            }
            else
            {
                AppStateManager.Message = successResult.Result.SuccessMessage;
                Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
                MudDialog.Close(DialogResult.Ok("success"));
            }
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
    }

    private void ShowRecoveryCodes()
    {
        DialogService.Show<ShowRecoveryCodes>();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    #endregion Private Methods
}