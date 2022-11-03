using D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
using D2W.Application.Features.Identity.Account.Commands.RegisterClient;
using D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;
using D2W.Application.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface IAccountUseCase
{
    #region Public Methods

    Task<Envelope<LoginResponse>> Login(LoginCommand request);

    Task<Envelope<SendLoginVerificationCodeResponse>> SendLoginVerificationCode(SendLoginVerificationCodeCommand request);

    Task<Envelope<LoginWithCodeResponse>> LoginWithCode(LoginWithCodeCommand request);

    Task<Envelope<LoginWith2FaResponse>> LoginWith2Fa(LoginWith2FaCommand request);

    Task<Envelope<LoginWithRecoveryCodeResponse>> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request);

    Task<Envelope<AuthResponse>> RefreshToken(RefreshTokenCommand request);

    Task<Envelope<RegisterResponse>> Register(RegisterCommand request);

    Task<Envelope<RegisterClientResponse>> RegisterClient(RegisterClientCommand request);

    Task<Envelope<RegisterWorkroomResponse>> RegisterWorkroom(RegisterWorkroomCommand request);

    Task<Envelope<string>> ConfirmEmail(string userId, string code);

    Task<Envelope<ForgetPasswordResponse>> ForgotPassword(ForgetPasswordCommand request);

    Task<Envelope<string>> ResetPassword(ResetPasswordCommand request);

    Task<Envelope<ResendEmailConfirmationResponse>> ResendEmailConfirmation(ResendEmailConfirmationCommand request);

    Task<bool> IsMaxBetaTestersReached();

    #endregion Public Methods
}