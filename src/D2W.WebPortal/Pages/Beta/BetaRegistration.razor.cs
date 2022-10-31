namespace D2W.WebPortal.Pages.Beta
{
    public partial class BetaRegistration
    {
        #region Private Properties

        // TODO validate subdomain and check if exists
        // TODO check if email exists

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
        private RegisterCommand RegisterCommand { get; set; } = new();

        #endregion Private Properties

        #region Private Methods

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

        private async Task RegisterUser()
        {
            Console.WriteLine("Beta RegisterUser invoked");

            SubmitButtonDisabled = true;
            RegisterCommand.AppUserType = ApplicationUserType.BetaTester;

            var httpResponseWrapper = await AccountsClient.Register(RegisterCommand);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<RegisterResponse>;
                if (successResult.Result.RequireConfirmedAccount)
                {
                    NavigationManager.NavigateTo(successResult.Result.DisplayConfirmAccountLink
                        ? $"account/registerConfirmation/{successResult.Result.DisplayConfirmAccountLink}/{successResult.Result.EmailConfirmationUrl}"
                        : $"account/registerConfirmation/{successResult.Result.DisplayConfirmAccountLink}");
                }
                else
                {
                    await AuthenticationService.Login(successResult.Result.AuthResponse);
                    var returnUrl = await ReturnUrlProvider.GetReturnUrl();
                    await ReturnUrlProvider.Clear();
                    NavigationManager.NavigateTo(returnUrl, forceLoad: true);
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

        #endregion Private Methods
    }
}
