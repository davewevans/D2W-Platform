using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Clients
{
    public partial class EditClient : ComponentBase
    {
        #region Public Properties

        public EditClient()
        {
            AddedApplicantReferencesList = new List<ReferenceItemForAdd>();
            ModifiedApplicantReferencesList = new List<ReferenceItemForEdit>();
            RemovedApplicantReferencesList = new List<string>();
        }

        [Parameter] public string ClientId { get; set; }

        public List<ReferenceItemForAdd> AddedApplicantReferencesList { get; set; }
        public List<ReferenceItemForEdit> ModifiedApplicantReferencesList { get; set; }
        public List<string> RemovedApplicantReferencesList { get; set; }

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
            new(Resource.Applicants, "/poc/army/applicants"),
            new(Resource.Edit_Applicant, "#", true)
        });

            var httpResponseWrapper = await ApplicantsClient.GetApplicant(new GetApplicantForEditQuery
            {
                Id = ClientId,
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
                    NewApplicantReferences = AddedApplicantReferencesList,
                    ModifiedApplicantReferences = ModifiedApplicantReferencesList,
                    RemovedApplicantReferences = RemovedApplicantReferencesList
                };
                var httpResponse = await ApplicantsClient.UpdateApplicant(UpdateApplicantCommand);

                if (httpResponse.Success)
                {
                    var successResult = httpResponse.Response as SuccessResult<string>;
                    Snackbar.Add(successResult.Result, Severity.Success);
                    NavigationManager.NavigateTo("poc/army/applicants");
                }
                else
                {
                    var exceptionResult = httpResponse.Response as ExceptionResult;
                    EditContextServerSideValidator.Validate(exceptionResult);
                    ServerSideValidator.Validate(exceptionResult);
                }
            }
        }

        private void RefreshNewApplicantReferencesList(List<ReferenceItemForAdd> referenceItems)
        {
            AddedApplicantReferencesList = referenceItems;
        }

        private void RefreshModifiedApplicantReferencesList(List<ReferenceItemForEdit> referenceItems)
        {
            ModifiedApplicantReferencesList = referenceItems;
        }

        private void RefreshRemovedApplicantReferencesList(List<string> referenceItemsIds)
        {
            RemovedApplicantReferencesList = referenceItemsIds;
        }

        #endregion Private Methods
    }
}
