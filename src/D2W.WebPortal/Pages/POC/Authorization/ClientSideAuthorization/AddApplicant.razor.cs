namespace D2W.WebPortal.Pages.POC.Authorization.ClientSideAuthorization;

public partial class AddApplicant
{
    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IAppStateManager AppStateManager { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private CreateApplicantCommand CreateApplicantCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Proof_of_Concepts, "#", true),
            new(Resource.Authorization, "#", true),
            new(Resource.Client_Side_Authorization, "#", true),
            new(Resource.Applicants, "/poc/authorization/clientSideAuthorization/applicants"),
            new(Resource.Add_Applicant, "#", true)
        });

        CreateApplicantCommand.DateOfBirth = DateTime.Now.AddYears(-18);
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task SubmitForm()
    {
        var httpResponseWrapper = await ApplicantsClient.CreateApplicant(CreateApplicantCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateApplicantResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            NavigationManager.NavigateTo("poc/authorization/clientSideAuthorization/applicants");
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