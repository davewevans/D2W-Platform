using D2W.WebPortal.Features.Identity.Account.Commands.LoginWithCodeCommand;
using D2W.WebPortal.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

namespace D2W.WebPortal.Consumers;

public class AccountsClient : IAccountsClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public AccountsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> RefreshToken(RefreshTokenCommand request)
    {
        return await _httpService.Post<RefreshTokenCommand, AuthResponse>("account/RefreshToken", request);
    }

    public async Task<HttpResponseWrapper<object>> Register(RegisterCommand request)
    {
        return await _httpService.Post<RegisterCommand, RegisterResponse>("account/Register", request);
    }

    public async Task<HttpResponseWrapper<object>> Login(LoginCommand request)
    {
        return await _httpService.Post<LoginCommand, LoginResponse>("account/Login", request);
    }

    public async Task<HttpResponseWrapper<object>> SendVerificationCode(SendLoginVerificationCodeCommand request)
    {
        return await _httpService.Post<SendLoginVerificationCodeCommand, SendLoginVerificationCodeResponse>("account/LoginSendVerificationCode", request);
    }

    public async Task<HttpResponseWrapper<object>> LoginWithVerificationCode(LoginWithCodeCommand request)
    {
        return await _httpService.Post<LoginWithCodeCommand, LoginWithCodeResponse>("account/LoginCodeVerification", request);
    }

    public async Task<HttpResponseWrapper<object>> LoginWith2Fa(LoginWith2FaCommand request)
    {
        return await _httpService.Post<LoginWith2FaCommand, LoginWith2FaResponse>("account/LoginWith2Fa", request);
    }

    public async Task<HttpResponseWrapper<object>> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request)
    {
        return await _httpService.Post<LoginWithRecoveryCodeCommand, LoginWithRecoveryCodeResponse>("account/LoginWithRecoveryCode", request);
    }

    public async Task<HttpResponseWrapper<object>> ForgetPassword(ForgetPasswordCommand request)
    {
        return await _httpService.Post<ForgetPasswordCommand, ForgetPasswordResponse>("account/ForgotPassword", request);
    }

    public async Task<HttpResponseWrapper<object>> ResetPassword(ResetPasswordCommand request)
    {
        return await _httpService.Post<ResetPasswordCommand, string>("account/ResetPassword", request);
    }

    public async Task<HttpResponseWrapper<object>> ConfirmEmail(ConfirmEmailCommand request)
    {
        return await _httpService.Post<ConfirmEmailCommand, string>("account/ConfirmEmail", request);
    }

    public async Task<HttpResponseWrapper<object>> ResendEmailConfirmation(ResendEmailConfirmationCommand request)
    {
        return await _httpService.Post<ResendEmailConfirmationCommand, ResendEmailConfirmationResponse>("account/ResendEmailConfirmation", request);
    }

    #endregion Public Methods
}