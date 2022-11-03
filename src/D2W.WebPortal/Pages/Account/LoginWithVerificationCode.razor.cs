using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Identity.Account.Commands.LoginWithCodeCommand;
using D2W.WebPortal.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

namespace D2W.WebPortal.Pages.Account;

public partial class LoginWithVerificationCode : ComponentBase
{
    #region Public Properties

    [Parameter]
    public string Username { get; set; } = string.Empty;

    #endregion Public Properties

    #region Private Properties

    [Inject] private IReturnUrlProvider ReturnUrlProvider { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private LoginWithCodeCommand LoginWithCodeCommand { get; set; } = new();
    private SendLoginVerificationCodeCommand SendVerificationCodeCommand {get; set; } = new();

    #endregion Private Properties

    // TODO send code

    protected override async Task OnParametersSetAsync()
    {
        await SendVerificationCode();
    }

    #region Private Methods

    private async Task SendVerificationCode()
    {
        SubmitButtonDisabled = true;

        SendVerificationCodeCommand.Email = Username;

        // TODO user can select Email or SMS

        SendVerificationCodeCommand.Provider = "Email";
        // RememberMachine
        // RememberMe
        // ReturnUrl

        var httpResponseWrapper = await AccountsClient.SendVerificationCode(SendVerificationCodeCommand);

         if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<SendLoginVerificationCodeResponse>;

            // TODO handle successful verification code sent

            SubmitButtonDisabled = false;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
            SubmitButtonDisabled = false;
        }

    }
    private async Task Submit()
    {
        SubmitButtonDisabled = true;

        LoginWithCodeCommand.Email = Username;
        LoginWithCodeCommand.Provider = "Email";
        LoginWithCodeCommand.TwoFactorCode.Trim();

        // RememberMachine
        // RememberMe
        // ReturnUrl

        var httpResponseWrapper = await AccountsClient.LoginWithVerificationCode(LoginWithCodeCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<LoginWithCodeResponse>;
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

