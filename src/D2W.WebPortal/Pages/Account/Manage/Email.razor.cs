namespace D2W.WebPortal.Pages.Account.Manage;

public partial class Email
{
    #region Public Properties

    [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

    #endregion Public Properties

    #region Private Properties

    private string _email { get; set; }

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ChangeEmailCommand ChangeEmailCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.GetUser();

        if (httpResponseWrapper.Success)
        {
            var authenticationState = await AuthenticationState;
            _email = authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task ChangeEmail()
    {
        var httpResponseWrapper = await ManageClient.ChangeEmail(ChangeEmailCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ChangeEmailResponse>;
            if (!successResult.Result.EmailUnchanged)
            {
                await AuthenticationService.Logout();
                await Redirect(successResult.Result);
            }
            else
            {
                Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            }
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private async Task Redirect(ChangeEmailResponse httpResponse)
    {
        if (httpResponse.RequireConfirmedAccount)
        {
            NavigationManager.NavigateTo(httpResponse.DisplayConfirmAccountLink
                ? $"account/manage/emailChangeConfirmation/{httpResponse.DisplayConfirmAccountLink}/{httpResponse.EmailConfirmationUrl}"
                : $"account/manage/emailChangeConfirmation/{httpResponse.DisplayConfirmAccountLink}");
        }
        else
        {
            await AuthenticationService.Login(httpResponse.AuthResponse);
        }
    }

    #endregion Private Methods
}