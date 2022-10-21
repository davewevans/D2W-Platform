namespace D2W.WebPortal.Pages.Settings;

public partial class IdentitySettings
{
    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IAppSettingsClient AppSettingsClient { get; set; }

    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private IdentitySettingsForEdit IdentitySettingsForEditVm { get; set; } = new();
    private UpdateIdentitySettingsCommand UpdateIdentitySettingsCommand { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Identity_Settings, "#",true)
        });

        var httpResponseWrapper = await AppSettingsClient.GetIdentitySettings();

        if (httpResponseWrapper.Success)
        {
            var identitySettingsForEdit = httpResponseWrapper.Response as SuccessResult<IdentitySettingsForEdit>;
            IdentitySettingsForEditVm = identitySettingsForEdit.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task SubmitForm()
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Are_you_sure_you_want_to_change_the_settings},
            {"ButtonText", Resource.Yes},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>("Confirm", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            UpdateIdentitySettingsCommand = new UpdateIdentitySettingsCommand
            {
                UserSettings = IdentitySettingsForEditVm.UserSettings,
                LockoutSettings = IdentitySettingsForEditVm.LockoutSettings,
                PasswordSettings = IdentitySettingsForEditVm.PasswordSettings,
                SignInSettings = IdentitySettingsForEditVm.SignInSettings,
            };

            var httpResponseWrapper = await AppSettingsClient.UpdateIdentitySettings(UpdateIdentitySettingsCommand);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<IdentitySettingsResponse>;
                IdentitySettingsForEditVm.UserSettings.Id = successResult.Result.UserSettingsId;
                IdentitySettingsForEditVm.LockoutSettings.Id = successResult.Result.LockoutSettingsId;
                IdentitySettingsForEditVm.PasswordSettings.Id = successResult.Result.PasswordSettingsId;
                IdentitySettingsForEditVm.SignInSettings.Id = successResult.Result.SignInSettingsId;
                Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
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