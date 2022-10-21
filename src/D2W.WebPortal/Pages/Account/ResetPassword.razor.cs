namespace D2W.WebPortal.Pages.Account;

public partial class ResetPassword
{
    #region Public Properties

    [Parameter] public string Code { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private bool PasswordVisibility { get; set; }
    private string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInput { get; set; } = InputType.Password;
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ResetPasswordCommand ResetPasswordCommand { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private async Task ResetUserPassword()
    {
        SubmitButtonDisabled = true;

        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("code", out var initialCount))
        {
            Code = Convert.ToString(initialCount);
        }

        ResetPasswordCommand.Code = Code;

        var httpResponseWrapper = await AccountsClient.ResetPassword(ResetPasswordCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<string>;
            Snackbar.Add(successResult.Result, Severity.Success);
            NavigationManager.NavigateTo("account/resetPasswordConfirmation");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
            SubmitButtonDisabled = false;
        }
    }

    private void TogglePasswordVisibility()
    {
        if (PasswordVisibility)
        {
            PasswordVisibility = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            PasswordVisibility = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    #endregion Private Methods
}