namespace D2W.WebPortal.Pages.POC.Authorization.ClientSideAuthorization;

public partial class ViewApplicant
{
    #region Public Properties

    [Parameter] public string ApplicantId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private ApplicantForEdit ApplicantForEditVm { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Proof_of_Concepts, "#", true),
            new(Resource.Authorization, "#", true),
            new(Resource.Client_Side_Authorization, "#", true),
            new(Resource.Applicants, "/poc/authorization/clientSideAuthorization/applicants"),
            new(Resource.View_Applicant, "#", true)
        });

        var httpResponseWrapper = await ApplicantsClient.GetApplicant(new GetApplicantForEditQuery
        {
            Id = ApplicantId,
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ApplicantForEdit>;
            ApplicantForEditVm = successResult?.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods
}