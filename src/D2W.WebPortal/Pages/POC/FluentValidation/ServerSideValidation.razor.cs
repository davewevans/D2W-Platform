namespace D2W.WebPortal.Pages.POC.FluentValidation;

public partial class ServerSideValidation
{
    #region Private Properties

    private bool IsTipsOpen { get; set; }

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }

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
            new(Resource.Fluent_Validation, "#", true),
            new(Resource.Server_Side_Validation, "#", true),
            new(Resource.Add_Applicant, "#", true)
        });

        CreateApplicantCommand.DateOfBirth = DateTime.Now.AddYears(-18);
    }

    #endregion Protected Methods

    #region Private Methods

    private void TipsToggle()
    {
        IsTipsOpen = !IsTipsOpen;
    }

    private async Task SubmitForm()
    {
        var httpResponseWrapper = await ApplicantsClient.CreateApplicant(CreateApplicantCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateApplicantResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            CreateApplicantCommand = new CreateApplicantCommand();
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