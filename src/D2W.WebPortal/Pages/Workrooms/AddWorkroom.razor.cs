using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Workrooms
{
    public partial class AddWorkroom : ComponentBase
    {
        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IWorkroomsClient WorkroomsWorkroom { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IAppStateManager AppStateManager { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private RegisterWorkroomCommand CreateWorkroomCommand { get; set; } = new();

        private bool IsTipsOpen { get; set; }

        #endregion Private Properties

        #region Protected Methods

        protected override void OnInitialized()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Applicants, "Workrooms"),
            new(Resource.Add_Applicant, "#", true)
        });

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
            var httpResponseWrapper = await WorkroomsWorkroom.CreateWorkroom(CreateWorkroomCommand);

            System.Console.WriteLine($"http response: {httpResponseWrapper.Success}");

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<RegisterWorkroomResponse>;

                if (successResult is not null)
                {
                    Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
                }
                else
                {
                    Snackbar.Add("result is null", Severity.Error);
                }

                NavigationManager.NavigateTo("Workrooms");
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
}
