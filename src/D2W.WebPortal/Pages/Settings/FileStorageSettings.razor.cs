namespace D2W.WebPortal.Pages.Settings;

public partial class FileStorageSettings
{
    #region Public Properties

    public bool StorageType { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IAppSettingsClient AppSettingsClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private UpdateFileStorageSettingsCommand UpdateFileStorageSettingsCommand { get; set; }
    private FileStorageSettingsForEdit FileStorageSettingsForEditVm { get; set; } = new();

    #endregion Private Properties

    #region Public Methods

    public void OnToggledChanged(bool toggled)
    {
        // Because variable is not two-way bound, we need to update it manually.
        StorageType = toggled;
        FileStorageSettingsForEditVm.StorageType = Convert.ToInt32(StorageType);
    }

    #endregion Public Methods

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.File_Storage_Settings, "#",true)
        });

        var httpResponseWrapper = await AppSettingsClient.GetFileStorageSettings();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<FileStorageSettingsForEdit>;
            FileStorageSettingsForEditVm = successResult.Result;
            StorageType = Convert.ToBoolean(FileStorageSettingsForEditVm.StorageType);
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
            UpdateFileStorageSettingsCommand = new UpdateFileStorageSettingsCommand
            {
                Id = FileStorageSettingsForEditVm.Id,
                StorageType = FileStorageSettingsForEditVm.StorageType
            };

            var httpResponseWrapper = await AppSettingsClient.UpdateFileStorageSettings(UpdateFileStorageSettingsCommand);

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<TokenSettingsResponse>;
                FileStorageSettingsForEditVm.Id = successResult.Result.Id;
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