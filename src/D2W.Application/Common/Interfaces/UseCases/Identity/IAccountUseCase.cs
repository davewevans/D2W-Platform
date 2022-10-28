namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface IAccountUseCase
{
    #region Public Methods

    Task<Envelope<LoginResponse>> Login(LoginCommand request);

    Task<Envelope<LoginWith2FaResponse>> LoginWith2Fa(LoginWith2FaCommand request);

    Task<Envelope<LoginWithRecoveryCodeResponse>> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request);

    Task<Envelope<AuthResponse>> RefreshToken(RefreshTokenCommand request);

    Task<Envelope<RegisterResponse>> Register(RegisterCommand request);

    Task<Envelope<string>> ConfirmEmail(string userId, string code);

    Task<Envelope<ForgetPasswordResponse>> ForgotPassword(ForgetPasswordCommand request);

    Task<Envelope<string>> ResetPassword(ResetPasswordCommand request);

    Task<Envelope<ResendEmailConfirmationResponse>> ResendEmailConfirmation(ResendEmailConfirmationCommand request);

    Task<bool> IsMaxBetaTestersReached();

    #endregion Public Methods
}