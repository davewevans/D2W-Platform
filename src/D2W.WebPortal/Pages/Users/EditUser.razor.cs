namespace D2W.WebPortal.Pages.Users;

public partial class EditUser
{
    #region Public Properties

    [Parameter] public string UserId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IUsersClient UsersClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string AvatarImageSrc { get; set; }
    private string CurrentEmail { get; set; }

    //private string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    //private bool PasswordVisibility { get; set; }
    //private InputType PasswordInput { get; set; } = InputType.Password;
    private bool SendActivationEmailDisabled { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private List<RoleItem> AssignedUserRoles { get; set; } = new();
    private StreamContent AvatarContent { get; set; }
    private List<StreamContent> AttachmentsContent { get; set; } = new();
    private List<AssignedUserAttachmentItem> UserAttachmentList { get; set; } = new();
    private UserForEdit UserForEditVm { get; set; } = new();
    private UpdateUserCommand UpdateUserCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Users, "/users"),
            new(Resource.Edit_User, "#", true)
        });

        var httpResponseWrapper = await UsersClient.GetUser(new GetUserForEditQuery { Id = UserId });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<UserForEdit>;

            UserForEditVm = successResult.Result;
            AssignedUserRoles = UserForEditVm.AssignedRoles.Select(ri => new RoleItem
            {
                Id = ri.Id,
                Name = ri.Name,
                IsDefault = ri.IsDefault,
                IsStatic = ri.IsStatic,
            }).ToList();

            UserAttachmentList = UserForEditVm.AssignedAttachments;

            if (!string.IsNullOrWhiteSpace(UserForEditVm.AvatarUri))
                UserForEditVm.IsAvatarAdded = true;

            if (UserForEditVm.AssignedAttachments != null)
                UserForEditVm.NumberOfAttachments = UserForEditVm.AssignedAttachments.Count;

            CurrentEmail = UserForEditVm.Email;
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
        UserForEditVm.IsAvatarAdded = true;
    }

    private void AvatarUnSelected()
    {
        AvatarContent = null;
        UserForEditVm.IsAvatarAdded = false;
    }

    private void DoNotSendActivationEmail(bool eventArg)
    {
        if (eventArg)
        {
            UserForEditVm.MustSendActivationEmail = false;
            SendActivationEmailDisabled = true;
        }
        else
        {
            SendActivationEmailDisabled = false;
        }
    }

    private void CanSendActivationEmail(string newEmail)
    {
        if (newEmail == CurrentEmail)
        {
            UserForEditVm.Email = CurrentEmail;
            UserForEditVm.MustSendActivationEmail = false;
            SendActivationEmailDisabled = true;
        }
        else
        {
            UserForEditVm.Email = newEmail;
            UserForEditVm.MustSendActivationEmail = true;
            SendActivationEmailDisabled = false;
        }
    }

    private void AttachmentSelected(StreamContent content)
    {
        UserForEditVm.NumberOfAttachments++;
        AttachmentsContent.Add(content);
    }

    private void AttachmentUnSelected(StreamContent content)
    {
        UserForEditVm.NumberOfAttachments--;
        AttachmentsContent.Remove(content);
    }

    private void RemoveFromUserCurrentAttachments(Guid removedUserAttachmentId)
    {
        UserAttachmentList.RemoveAll(ua => ua.Id == removedUserAttachmentId);
        UserForEditVm.NumberOfAttachments--;
    }

    private void UpdateUserRoles(List<RoleItem> updatedUserRoles)
    {
        AssignedUserRoles = updatedUserRoles.Select(ur => new RoleItem
        {
            Id = ur.Id,
            Name = ur.Name,
            IsDefault = ur.IsDefault,
            IsStatic = ur.IsStatic,
            CreatedOn = ur.CreatedOn,
            CreatedBy = ur.CreatedBy,
            ModifiedOn = ur.ModifiedOn,
            ModifiedBy = ur.ModifiedBy
        }).ToList();
    }

    private async Task SubmitForm()
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
            UpdateUserCommand = new UpdateUserCommand
            {
                Id = UserForEditVm.Id,
                Email = UserForEditVm.Email,
                Name = UserForEditVm.Name,
                Surname = UserForEditVm.Surname,
                JobTitle = UserForEditVm.JobTitle,
                PhoneNumber = UserForEditVm.PhoneNumber,
                Password = UserForEditVm.Password,
                ConfirmPassword = UserForEditVm.ConfirmPassword,
                AvatarUri = UserForEditVm.AvatarUri,
                IsAvatarAdded = UserForEditVm.IsAvatarAdded,
                IsSuspended = UserForEditVm.IsSuspended,
                MustSendActivationEmail = UserForEditVm.MustSendActivationEmail,
                SetRandomPassword = UserForEditVm.SetRandomPassword,
                NumberOfAttachments = UserForEditVm.NumberOfAttachments,
                AssignedRoleIds = AssignedUserRoles.Select(ar => ar.Id).ToList(),
                AttachmentIds = UserAttachmentList.Select(ai => ai.Id).ToList(),
            };

            var userFormData = new MultipartFormDataContent
            {
                { new StringContent(UpdateUserCommand.Id ?? string.Empty), "id" },
                { new StringContent(UpdateUserCommand.Name ?? string.Empty), "Name" },
                { new StringContent(UpdateUserCommand.Surname ?? string.Empty), "Surname" },
                { new StringContent(UpdateUserCommand.JobTitle ?? string.Empty), "JobTitle" },
                { new StringContent(UpdateUserCommand.Email ?? string.Empty), "Email" },
                { new StringContent(UpdateUserCommand.PhoneNumber ?? string.Empty), "PhoneNumber" },
                { new StringContent(UpdateUserCommand.Password ?? string.Empty), "Password" },
                { new StringContent(UpdateUserCommand.ConfirmPassword ?? string.Empty), "ConfirmPassword" },
                { new StringContent(UpdateUserCommand.AvatarUri ?? string.Empty), "AvatarUri" },
                { new StringContent(UpdateUserCommand.SetRandomPassword.ToString()), "SetRandomPassword" },
                { new StringContent(UpdateUserCommand.MustSendActivationEmail.ToString()), "MustSendActivationEmail" },
                { new StringContent(UpdateUserCommand.IsSuspended.ToString()), "IsSuspended" },
                { new StringContent(UpdateUserCommand.IsAvatarAdded.ToString()), "IsAvatarAdded" },
                { new StringContent(UpdateUserCommand.NumberOfAttachments.ToString()), "NumberOfAttachments" }
            };

            if (AvatarContent != null)
                userFormData.Add(AvatarContent, "Avatar", AvatarContent.Headers.GetValues("FileName").LastOrDefault());

            foreach (var item in AttachmentsContent)
                userFormData.Add(item, "Attachments", item.Headers.GetValues("FileName").LastOrDefault());

            foreach (var item in UpdateUserCommand.AssignedRoleIds)
                userFormData.Add(new StringContent(item), "AssignedRoleIds");

            foreach (var item in UpdateUserCommand.AttachmentIds)
                userFormData.Add(new StringContent(item.ToString()), "AttachmentIds");

            var httpResponse = await UsersClient.UpdateUserFormData(userFormData);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("users");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    #endregion Private Methods

    //private void TogglePasswordVisibility()
    //{
    //    if (PasswordVisibility)
    //    {
    //        PasswordVisibility = false;
    //        PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    //        PasswordInput = InputType.Password;
    //    }
    //    else
    //    {
    //        PasswordVisibility = true;
    //        PasswordInputIcon = Icons.Material.Filled.Visibility;
    //        PasswordInput = InputType.Text;
    //    }
    //}
}