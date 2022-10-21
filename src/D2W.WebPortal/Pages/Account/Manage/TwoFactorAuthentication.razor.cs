namespace D2W.WebPortal.Pages.Account.Manage;

public partial class TwoFactorAuthentication
{
    #region Public Properties

    [Parameter] public string SuccessMessage { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private Get2FaStateResponse Get2FaStateResponse { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.Get2FaState();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<Get2FaStateResponse>;
            Get2FaStateResponse = successResult.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task DisableAuthenticator()
    {
        var dialog = DialogService.Show<DisableTwoFactorAuthentication>();

        var result = await dialog.Result;

        if (!result.Cancelled && result.Data.ToString() == "success")
        {
            var httpResponseWrapper = await ManageClient.Get2FaState();

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<Get2FaStateResponse>;
                Get2FaStateResponse = successResult.Result;
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    private async Task ResetAuthenticator()
    {
        var dialog = DialogService.Show<ResetAuthenticator>();

        var result = await dialog.Result;

        if (!result.Cancelled && result.Data.ToString() == "success")
        {
            var httpResponseWrapper = await ManageClient.Get2FaState();

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<Get2FaStateResponse>;
                Get2FaStateResponse = successResult.Result;
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    private async Task EnableAuthenticator()
    {
        var dialog = DialogService.Show<EnableAuthenticator>();

        var result = await dialog.Result;

        if (!result.Cancelled && result.Data.ToString() == "success")
        {
            var httpResponseWrapper = await ManageClient.Get2FaState();

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<Get2FaStateResponse>;
                Get2FaStateResponse = successResult.Result;
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    private async Task GenerateRecoveryCodes()
    {
        var dialog = DialogService.Show<GenerateRecoveryCodes>();

        var result = await dialog.Result;

        if (!result.Cancelled && result.Data.ToString() == "success")
        {
            var httpResponseWrapper = await ManageClient.Get2FaState();

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<Get2FaStateResponse>;
                Get2FaStateResponse = successResult.Result;
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    #endregion Private Methods
}