namespace D2W.WebPortal.Pages.Settings;

public partial class TokenSettings
{
    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IAppSettingsClient AppSettingsClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private TokenSettingsForEdit TokenSettingsForEditVm { get; set; } = new();
    private UpdateTokenSettingsCommand UpdateTokenSettingsCommand { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Token_Settings, "#",true)
        });

        var httpResponseWrapper = await AppSettingsClient.GetTokenSettings();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<TokenSettingsForEdit>;
            TokenSettingsForEditVm = successResult.Result;
            TokenSettingsForEditVm.TokenSettings.AccessTokenUoT = 0;
            TokenSettingsForEditVm.TokenSettings.RefreshTokenUoT = 0;
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
            if (TokenSettingsForEditVm.TokenSettings.AccessTokenTimeSpan != null)
            {
                var time = TokenSettingsForEditVm.TokenSettings.AccessTokenTimeSpan.Value;
                switch ((UoT)TokenSettingsForEditVm.TokenSettings.AccessTokenUoT)
                {
                    case UoT.Hours:
                        TokenSettingsForEditVm.TokenSettings.AccessTokenTimeSpan = TimeSpan.FromHours(time).TotalMinutes;
                        break;

                    case UoT.Days:
                        TokenSettingsForEditVm.TokenSettings.AccessTokenTimeSpan = TimeSpan.FromDays(time).TotalMinutes;
                        break;

                    case UoT.Month:
                        time *= 30;
                        TokenSettingsForEditVm.TokenSettings.AccessTokenTimeSpan = TimeSpan.FromDays(time).TotalMinutes;
                        break;

                    case UoT.Minutes:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (TokenSettingsForEditVm.TokenSettings.RefreshTokenTimeSpan != null)
            {
                var time = TokenSettingsForEditVm.TokenSettings.RefreshTokenTimeSpan.Value;
                switch ((UoT)TokenSettingsForEditVm.TokenSettings.RefreshTokenUoT)
                {
                    case UoT.Hours:
                        TokenSettingsForEditVm.TokenSettings.RefreshTokenTimeSpan = TimeSpan.FromHours(time).TotalMinutes;
                        break;

                    case UoT.Days:
                        TokenSettingsForEditVm.TokenSettings.RefreshTokenTimeSpan = TimeSpan.FromDays(time).TotalMinutes;
                        break;

                    case UoT.Month:
                        time *= 30;
                        TokenSettingsForEditVm.TokenSettings.RefreshTokenTimeSpan = TimeSpan.FromDays(time).TotalMinutes;
                        break;

                    case UoT.Minutes:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            UpdateTokenSettingsCommand = new UpdateTokenSettingsCommand
            {
                TokenSettings = TokenSettingsForEditVm.TokenSettings,
            };

            var httpResponseWrapper = await AppSettingsClient.UpdateTokenSettings(UpdateTokenSettingsCommand);

            if (httpResponseWrapper.Success)
            {
                TokenSettingsForEditVm.TokenSettings.AccessTokenUoT = 0;
                TokenSettingsForEditVm.TokenSettings.RefreshTokenUoT = 0;
                var successResult = httpResponseWrapper.Response as SuccessResult<TokenSettingsResponse>;
                TokenSettingsForEditVm.TokenSettings.Id = successResult.Result.Id;
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