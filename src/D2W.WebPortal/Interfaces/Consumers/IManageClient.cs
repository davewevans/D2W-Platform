namespace D2W.WebPortal.Interfaces.Consumers;

public interface IManageClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> ChangeEmail(ChangeEmailCommand request);

    Task<HttpResponseWrapper<object>> ChangePassword(ChangePasswordCommand request);

    Task<HttpResponseWrapper<object>> CheckUser2FaState();

    Task<HttpResponseWrapper<object>> ConfirmEmailChange(ConfirmEmailChangeCommand request);

    Task<HttpResponseWrapper<object>> DeletePersonalData(DeletePersonalDataCommand request);

    Task<HttpResponseWrapper<object>> Disable2Fa();

    Task<HttpResponseWrapper<object>> DownloadPersonalData();

    Task<HttpResponseWrapper<object>> EnableAuthenticator(EnableAuthenticatorCommand request);

    Task<HttpResponseWrapper<object>> GenerateRecoveryCodes();

    Task<HttpResponseWrapper<object>> GetUser();

    Task<HttpResponseWrapper<object>> GetUserAvatar();

    Task<HttpResponseWrapper<object>> Get2FaState();

    Task<HttpResponseWrapper<object>> LoadSharedKeyAndQrCodeUri();

    Task<HttpResponseWrapper<object>> RequirePassword();

    Task<HttpResponseWrapper<object>> ResetAuthenticator();

    Task<HttpResponseWrapper<object>> UpdateUserProfile(UpdateUserProfileCommand request);

    Task<HttpResponseWrapper<object>> UpdateUserAvatarFormData(MultipartFormDataContent request);

    #endregion Public Methods
}