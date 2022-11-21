using D2W.WebPortal.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.WebPortal.Features.Workrooms.Queries.GetWorkroomForEdit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Workrooms
{
    public partial class EditWorkroom : ComponentBase
    {
        #region Public Properties

        [Parameter] public string WorkroomId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IWorkroomsClient WorkroomsWorkroom { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private WorkroomForEdit WorkroomForEditVm { get; set; } = new();
        private UpdateWorkroomCommand UpdateWorkroomCommand { get; set; }

        private string _country = string.Empty;

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Workrooms, "workrooms"),
                new(Resource.Edit_Workroom, "#", true)
            });

            System.Console.WriteLine("workroom id: " + WorkroomId);

            var httpResponseWrapper = await WorkroomsWorkroom.GetWorkroom(new GetWorkroomForEditQuery
            {
                Id = WorkroomId,
            });

            if (httpResponseWrapper.Success)
            {
                System.Console.WriteLine("httpResponseWrapper.Success");
                var successResult = httpResponseWrapper.Response as SuccessResult<WorkroomForEdit>;
                WorkroomForEditVm = successResult?.Result;

                var country = WorkroomForEditVm?.Countries.FirstOrDefault(x => x.Id.Equals(WorkroomForEditVm.CountryId));
                if (country is not null)
                    _country = country.CountryName;
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
            var country = WorkroomForEditVm?.Countries.FirstOrDefault(x => x.CountryName.Equals(_country));

            UpdateWorkroomCommand = new UpdateWorkroomCommand
            {
                Id = WorkroomForEditVm.Id,
                CompanyName = WorkroomForEditVm.CompanyName,
                PhoneNumber = WorkroomForEditVm.PhoneNumber,
                EmailAddress = WorkroomForEditVm.EmailAddress,
                AltEmailAddress = WorkroomForEditVm.AltEmailAddress,
                AltPhoneNumber = WorkroomForEditVm.AltPhoneNumber,
                Fax = WorkroomForEditVm.Fax,
                AddressLine1 = WorkroomForEditVm.AddressLine1,
                AddressLine2 = WorkroomForEditVm.AddressLine2,
                City = WorkroomForEditVm.City,
                Region = WorkroomForEditVm.Region,
                PostalCode = WorkroomForEditVm.PostalCode,
                CountryId = country is not null ? country.Id : null
            };
            var httpResponse = await WorkroomsWorkroom.UpdateWorkroom(UpdateWorkroomCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("workrooms");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
        #endregion Private Methods
    }
}
