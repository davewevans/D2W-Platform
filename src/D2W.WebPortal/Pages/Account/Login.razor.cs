namespace D2W.WebPortal.Pages.Account;

public partial class Login
{
    #region Private Properties

    [Inject] private IReturnUrlProvider ReturnUrlProvider { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private bool PasswordVisibility { get; set; }
    private string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInput { get; set; } = InputType.Password;
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private LoginCommand LoginCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        LoginCommand.Email = "stumeltyb@networksolutions.com";
        LoginCommand.Password = "P@ssword123";
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task LoginUser()
    {
        SubmitButtonDisabled = true;

        var httpResponseWrapper = await AccountsClient.Login(LoginCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<LoginResponse>;

            if (successResult.Result.RequiresTwoFactor)
            {
                NavigationManager.NavigateTo($"account/loginWith2Fa/{LoginCommand.Email}");
            }
            else
            {
                await AuthenticationService.Login(successResult.Result.AuthResponse);
                var returnUrl = await ReturnUrlProvider.GetReturnUrl();
                await ReturnUrlProvider.Clear();
                NavigationManager.NavigateTo(returnUrl);
            }
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