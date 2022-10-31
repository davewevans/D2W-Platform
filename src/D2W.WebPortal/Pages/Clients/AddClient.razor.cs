using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Clients
{
    public partial class AddClient : ComponentBase
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

    private bool IsTipsOpen { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Applicants, "poc/army/applicants"),
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

    private async void AppStateChanged(object sender, EventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }

    private async Task SubmitForm()
    {
        var httpResponseWrapper = await ApplicantsClient.CreateApplicant(CreateApplicantCommand);
        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateApplicantResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            NavigationManager.NavigateTo("poc/army/applicants");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private void UpdateApplicantReferences(List<ReferenceItemForAdd> referenceItems)
    {
        CreateApplicantCommand.ReferenceItems = referenceItems;
    }

    #endregion Private Methods
    }
}
