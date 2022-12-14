using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Identity.Account.Commands.Register;

namespace D2W.WebPortal.Pages.Clients
{
    public partial class AddClient : ComponentBase
    {
        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IClientsClient ClientsClient { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IAppStateManager AppStateManager { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private RegisterClientCommand CreateClientCommand { get; set; } = new();

        private bool IsTipsOpen { get; set; }

        #endregion Private Properties

        #region Protected Methods

        protected override void OnInitialized()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Applicants, "Clients"),
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
            var httpResponseWrapper = await ClientsClient.CreateClient(CreateClientCommand);

            System.Console.WriteLine($"http response: {httpResponseWrapper.Success}");

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<RegisterClientResponse>;

                if (successResult is not null)
                {
                    Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
                }
                else
                {
                    Snackbar.Add("result is null", Severity.Error);
                }

                NavigationManager.NavigateTo("Clients");
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
