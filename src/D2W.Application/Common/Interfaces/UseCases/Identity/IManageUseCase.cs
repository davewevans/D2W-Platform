namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface IManageUseCase
{
    #region Public Methods

    Task<Envelope<ChangeEmailResponse>> ChangeEmail(ChangeEmailCommand request);

    Task<Envelope<bool>> RequirePassword();

    Task<Envelope<ChangePasswordResponse>> ChangePassword(ChangePasswordCommand request);

    Task<Envelope<SetPasswordResponse>> SetPassword(SetPasswordCommand request);

    Task<Envelope<string>> Disable2Fa();

    Task<Envelope<CurrentUserForEdit>> GetCurrentUser();

    Task<Envelope<DownloadPersonalDataResponse>> DownloadPersonalData();

    Task<Envelope<string>> DeletePersonalData(DeletePersonalDataCommand request);

    Task<Envelope<EnableAuthenticatorResponse>> EnableAuthenticator(EnableAuthenticatorCommand request);

    Task<Envelope<ResetAuthenticatorResponse>> ResetAuthenticator();

    Task<Envelope<Get2FaStateResponse>> GetTwoFactorAuthenticationState();

    Task<Envelope<GenerateRecoveryCodesResponse>> GenerateRecoveryCodes();

    Task<Envelope<string>> UpdateUserProfile(UpdateUserProfileCommand request);

    Task<Envelope<ChangeEmailResponse>> ConfirmEmailChange(string userId, string email, string code);

    Task<Envelope<LoadSharedKeyAndQrCodeUriResponse>> LoadSharedKeyAndQrCodeUri();

    Task<Envelope<User2FaStateResponse>> CheckUser2FaState();

    Task<Envelope<UserAvatarForEdit>> GetUserAvatar();

    Task<Envelope<string>> UpdateUserAvatar(UpdateUserAvatarCommand request);

    #endregion Public Methods
}