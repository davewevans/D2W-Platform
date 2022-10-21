namespace D2W.WebPortal.Pages.Account.Manage;

public partial class Avatar
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }

    private string AvatarImageSrc { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private UpdateUserAvatarCommand UpdateUserAvatarCommand { get; set; } = new();
    private UserAvatarForEdit UserAvatarForEditVm { get; set; } = new();
    private StreamContent AvatarContent { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.GetUserAvatar();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<UserAvatarForEdit>;
            UserAvatarForEditVm = successResult.Result;
            if (!string.IsNullOrWhiteSpace(UserAvatarForEditVm.AvatarUri))
                UserAvatarForEditVm.IsAvatarAdded = true;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private void GetBase64StringImageUrl(string avatarImageSrc)
    {
        AvatarImageSrc = avatarImageSrc;
    }

    private void AvatarSelected(StreamContent content)
    {
        AvatarContent = content;
        UserAvatarForEditVm.IsAvatarAdded = true;
    }

    private void AvatarUnSelected(StreamContent content)
    {
        AvatarContent = null;
        UserAvatarForEditVm.IsAvatarAdded = false;
    }

    private async Task UpdateUserAvatar()
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Are_you_sure_you_want_to_save_user_profile},
            {"ButtonText", Resource.Yes},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>("Confirm", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            UpdateUserAvatarCommand = new UpdateUserAvatarCommand
            {
                AvatarUri = UserAvatarForEditVm.AvatarUri,
                IsAvatarAdded = UserAvatarForEditVm.IsAvatarAdded,
            };

            var userFormData = new MultipartFormDataContent
            {
                { new StringContent(UpdateUserAvatarCommand.AvatarUri ?? string.Empty), "AvatarUri" },
                { new StringContent(UpdateUserAvatarCommand.IsAvatarAdded.ToString()), "IsAvatarAdded" },
            };

            if (AvatarContent != null)
                userFormData.Add(AvatarContent, Resource.Avatar, AvatarContent.Headers.GetValues("FileName").LastOrDefault() ?? string.Empty);

            var httpResponseWrapper = await ManageClient.UpdateUserAvatarFormData(userFormData);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    #endregion Private Methods
}