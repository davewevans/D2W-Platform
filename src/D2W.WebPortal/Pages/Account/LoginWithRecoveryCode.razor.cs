
using D2W.WebPortal.Features.Identity.Account.Commands.LoginWithCodeCommand;
namespace D2W.WebPortal.Pages.Account;

public partial class LoginWithRecoveryCode
{
    #region Public Properties

    [Parameter] public string Username { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IReturnUrlProvider ReturnUrlProvider { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private LoginWithRecoveryCodeCommand LoginWithRecoveryCodeCommand { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private async Task LoginWith2FaRecoveryCode()
    {
        SubmitButtonDisabled = true;

        LoginWithRecoveryCodeCommand.UserName = Username;
        

        var httpResponseWrapper = await AccountsClient.LoginWithRecoveryCode(LoginWithRecoveryCodeCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<LoginWithRecoveryCodeResponse>;
            await AuthenticationService.Login(successResult.Result.AuthResponse);
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