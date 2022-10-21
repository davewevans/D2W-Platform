namespace D2W.WebPortal.Pages.Account.Manage;

public partial class Profile
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private CurrentUserForEdit UserForEdit { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        var httpResponseWrapper = await ManageClient.GetUser();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CurrentUserForEdit>;
            UserForEdit = successResult.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task UpdateUserProfile()
    {
        var updateCurrentUserProfileCommand = new UpdateUserProfileCommand
        {
            Name = UserForEdit.Name,
            Surname = UserForEdit.Surname,
            JobTitle = UserForEdit.JobTitle,
            PhoneNumber = UserForEdit.PhoneNumber
        };

        var httpResponseWrapper = await ManageClient.UpdateUserProfile(updateCurrentUserProfileCommand);

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

    #endregion Private Methods
}