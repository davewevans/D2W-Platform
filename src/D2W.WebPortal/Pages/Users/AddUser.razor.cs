namespace D2W.WebPortal.Pages.Users;

public partial class AddUser
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IUsersClient UsersClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string AvatarImageSrc { get; set; }
    private bool PasswordVisibility { get; set; }
    private string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInput { get; set; } = InputType.Password;
    private bool SendActivationEmailDisabled { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private StreamContent AvatarContent { get; set; }
    private List<StreamContent> AttachmentsContent { get; set; } = new();
    private List<RoleItem> AssignedRoles { get; set; } = new();
    private CreateUserCommand CreateUserCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Users, "/users"),
            new(Resource.Add_User, "#", true)
        });
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
        CreateUserCommand.IsAvatarAdded = true;
    }

    private void AvatarUnSelected(StreamContent content)
    {
        AvatarContent = null;
        CreateUserCommand.IsAvatarAdded = false;
    }

    private void DoNotSendActivationEmail(bool eventArg)
    {
        if (eventArg)
        {
            CreateUserCommand.MustSendActivationEmail = false;
            SendActivationEmailDisabled = true;
        }
        else
        {
            SendActivationEmailDisabled = false;
        }
    }

    private void UpdateUserRoles(List<RoleItem> updatedUserRoles)
    {
        AssignedRoles = updatedUserRoles.Select(ri => new RoleItem
        {
            Id = ri.Id,
            Name = ri.Name,
            IsDefault = ri.IsDefault,
            IsStatic = ri.IsStatic,
            CreatedOn = ri.CreatedOn,
            CreatedBy = ri.CreatedBy,
            ModifiedOn = ri.ModifiedOn,
            ModifiedBy = ri.ModifiedBy
        }).ToList();
    }

    private void AttachmentSelected(StreamContent content)
    {
        CreateUserCommand.NumberOfAttachments++;
        AttachmentsContent.Add(content);
    }

    private void AttachmentUnSelected(StreamContent content)
    {
        CreateUserCommand.NumberOfAttachments--;
        AttachmentsContent.Remove(content);
    }

    private async Task SubmitForm()
    {
        CreateUserCommand.AssignedRoleIds = AssignedRoles.Select(ar => ar.Id).ToList();

        using var userFormData = new MultipartFormDataContent
        {
            {new StringContent(CreateUserCommand.IsAvatarAdded.ToString()), "IsAvatarAdded"},
            {new StringContent(CreateUserCommand.NumberOfAttachments.ToString()), "NumberOfAttachments"}
        };

        if (AvatarContent != null)
        {
            userFormData.Add(AvatarContent, "Avatar", AvatarContent.Headers.GetValues("FileName").LastOrDefault());
        }

        userFormData.Add(new StringContent(CreateUserCommand.Name ?? string.Empty), "Name");
        userFormData.Add(new StringContent(CreateUserCommand.Surname ?? string.Empty), "Surname");
        userFormData.Add(new StringContent(CreateUserCommand.JobTitle ?? string.Empty), "JobTitle");
        userFormData.Add(new StringContent(CreateUserCommand.Email ?? string.Empty), "Email");
        userFormData.Add(new StringContent(CreateUserCommand.PhoneNumber ?? string.Empty), "PhoneNumber");
        userFormData.Add(new StringContent(CreateUserCommand.Password ?? string.Empty), "Password");
        userFormData.Add(new StringContent(CreateUserCommand.ConfirmPassword ?? string.Empty), "ConfirmPassword");
        userFormData.Add(new StringContent(CreateUserCommand.SetRandomPassword.ToString()), "SetRandomPassword");
        userFormData.Add(new StringContent(CreateUserCommand.MustSendActivationEmail.ToString()), "MustSendActivationEmail");
        userFormData.Add(new StringContent(CreateUserCommand.IsSuspended.ToString()), "IsSuspended");

        if (AttachmentsContent != null)
            foreach (var item in AttachmentsContent)
                userFormData.Add(item, "Attachments", item.Headers.GetValues("FileName").LastOrDefault());

        foreach (var item in CreateUserCommand.AssignedRoleIds)
            userFormData.Add(new StringContent(item), "AssignedRoleIds");

        var httpResponseWrapper = await UsersClient.CreateUserFormData(userFormData);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateUserResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            NavigationManager.NavigateTo("/users");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private void TogglePasswordVisibility()
    {
        if (PasswordVisibility)
        {
            PasswordVisibility = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            PasswordVisibility = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    #endregion Private Methods
}