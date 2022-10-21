namespace D2W.WebPortal.Pages.Account;

public partial class ResendEmailConfirmation
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool SubmitButtonDisabled { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ResendEmailConfirmationCommand ResendEmailConfirmationCommand { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private async Task ResendConfirmation()
    {
        SubmitButtonDisabled = true;

        var httpResponseWrapper = await AccountsClient.ResendEmailConfirmation(ResendEmailConfirmationCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ResendEmailConfirmationResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            if (successResult.Result.RequireConfirmedAccount)
            {
                NavigationManager.NavigateTo(successResult.Result.DisplayConfirmAccountLink
                    ? $"account/registerConfirmation/{successResult.Result.DisplayConfirmAccountLink}/{successResult.Result.EmailConfirmationUrl}"
                    : $"account/registerConfirmation/{successResult.Result.DisplayConfirmAccountLink}");
            }
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
            SubmitButtonDisabled = false;
        }
    }

    #endregion Private Methods
}