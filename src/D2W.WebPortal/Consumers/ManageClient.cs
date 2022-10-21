namespace D2W.WebPortal.Consumers;

public class ManageClient : IManageClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public ManageClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> GetUser()
    {
        return await _httpService.Get<CurrentUserForEdit>("manage/GetCurrentUser");
    }

    public async Task<HttpResponseWrapper<object>> GetUserAvatar()
    {
        return await _httpService.Get<UserAvatarForEdit>("manage/GetUserAvatar");
    }

    public async Task<HttpResponseWrapper<object>> UpdateUserProfile(UpdateUserProfileCommand request)
    {
        return await _httpService.Put<UpdateUserProfileCommand, string>("manage/UpdateUserProfile", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateUserAvatarFormData(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, string>("manage/UpdateUserAvatar", request);
    }

    public async Task<HttpResponseWrapper<object>> ChangeEmail(ChangeEmailCommand request)
    {
        return await _httpService.Post<ChangeEmailCommand, ChangeEmailResponse>("manage/ChangeEmail", request);
    }

    public async Task<HttpResponseWrapper<object>> ConfirmEmailChange(ConfirmEmailChangeCommand request)
    {
        return await _httpService.Put<ConfirmEmailChangeCommand, ChangeEmailResponse>("manage/ConfirmEmailChange", request);
    }

    public async Task<HttpResponseWrapper<object>> ChangePassword(ChangePasswordCommand request)
    {
        return await _httpService.Post<ChangePasswordCommand, ChangePasswordResponse>("manage/ChangePassword", request);
    }

    public async Task<HttpResponseWrapper<object>> Get2FaState()
    {
        return await _httpService.Get<Get2FaStateResponse>("manage/Get2FaState");
    }

    public async Task<HttpResponseWrapper<object>> LoadSharedKeyAndQrCodeUri()
    {
        return await _httpService.Get<LoadSharedKeyAndQrCodeUriResponse>("manage/LoadSharedKeyAndQrCodeUri");
    }

    public async Task<HttpResponseWrapper<object>> EnableAuthenticator(EnableAuthenticatorCommand request)
    {
        return await _httpService.Post<EnableAuthenticatorCommand, EnableAuthenticatorResponse>("manage/EnableAuthenticator", request);
    }

    public async Task<HttpResponseWrapper<object>> Disable2Fa()
    {
        return await _httpService.Post<string>("manage/Disable2Fa");
    }

    public async Task<HttpResponseWrapper<object>> GenerateRecoveryCodes()
    {
        return await _httpService.Get<GenerateRecoveryCodesResponse>("manage/GenerateRecoveryCodes");
    }

    public async Task<HttpResponseWrapper<object>> CheckUser2FaState()
    {
        return await _httpService.Get<User2FaStateResponse>("manage/CheckUser2FaState");
    }

    public async Task<HttpResponseWrapper<object>> ResetAuthenticator()
    {
        return await _httpService.Post<ResetAuthenticatorResponse>("manage/ResetAuthenticator");
    }

    public async Task<HttpResponseWrapper<object>> DownloadPersonalData()
    {
        return await _httpService.Get<DownloadPersonalDataResponse>("manage/DownloadPersonalData");
    }

    public async Task<HttpResponseWrapper<object>> DeletePersonalData(DeletePersonalDataCommand request)
    {
        return await _httpService.Post<DeletePersonalDataCommand, string>("manage/DeletePersonalData", request);
    }

    public async Task<HttpResponseWrapper<object>> RequirePassword()
    {
        return await _httpService.Get<bool>("manage/RequirePassword");
    }

    #endregion Public Methods
}