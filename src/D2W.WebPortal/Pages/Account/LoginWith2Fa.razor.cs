namespace D2W.WebPortal.Pages.Account;

public partial class LoginWith2Fa
{
    #region Public Properties

    [Parameter] public string UserName { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IReturnUrlProvider ReturnUrlProvider { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private string RecoveryCodeUrl { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private LoginWith2FaCommand LoginWith2FaCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        RecoveryCodeUrl = $"/account/LoginWithRecoveryCode/{UserName}";
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task LoginWith2FaUser()
    {
        SubmitButtonDisabled = true;

        LoginWith2FaCommand.UserName = UserName;

        var httpResponseWrapper = await AccountsClient.LoginWith2Fa(LoginWith2FaCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<LoginWith2FaResponse>;
            await AuthenticationService.ReAuthenticate(successResult.Result.AuthResponse);
            var returnUrl = await ReturnUrlProvider.GetReturnUrl();
            await ReturnUrlProvider.Clear();
            NavigationManager.NavigateTo(returnUrl);
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