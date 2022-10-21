namespace D2W.WebPortal.Pages.Account;

public partial class ForgotPassword
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ForgetPasswordCommand ForgetPasswordCommand { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private async Task ForgetPassword()
    {
        SubmitButtonDisabled = true;

        var httpResponseWrapper = await AccountsClient.ForgetPassword(ForgetPasswordCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ForgetPasswordResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            var token = !string.IsNullOrEmpty(successResult.Result.Code) ? successResult.Result.Code : Guid.NewGuid().ToString();
            NavigationManager.NavigateTo($"/account/forgotPasswordConfirmation/{token}");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
            SubmitButtonDisabled = false;
        }
    }

    #endregion Private Methods
}