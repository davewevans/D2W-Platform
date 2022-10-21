namespace D2W.WebPortal.Pages.POC.Authorization.ClientSideAuthorization;

public partial class EditApplicant
{
    #region Public Properties

    [Parameter] public string ApplicantId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private ApplicantForEdit ApplicantForEditVm { get; set; } = new();
    private UpdateApplicantCommand UpdateApplicantCommand { get; set; }

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
            new(Resource.Edit_Applicant, "#", true)
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

    #region Private Methods

    private async Task SubmitForm()
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Are_you_sure_you_want_to_save_applicant},
            {"ButtonText", Resource.Yes},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>("Confirm", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            UpdateApplicantCommand = new UpdateApplicantCommand
            {
                Id = ApplicantForEditVm.Id,
                Ssn = ApplicantForEditVm.Ssn,
                FirstName = ApplicantForEditVm.FirstName,
                LastName = ApplicantForEditVm.LastName,
                DateOfBirth = ApplicantForEditVm.DateOfBirth,
                Height = ApplicantForEditVm.Height,
                Weight = ApplicantForEditVm.Weight,
            };
            var httpResponse = await ApplicantsClient.UpdateApplicant(UpdateApplicantCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("poc/authorization/clientSideAuthorization/applicants");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
    }

    #endregion Private Methods
}