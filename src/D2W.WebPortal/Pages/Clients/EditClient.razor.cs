using D2W.WebPortal.Features.Clients.Commands.UpdateClient;
using D2W.WebPortal.Features.Clients.Queries.GetClientForEdit;
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

        [Parameter] public string ClientId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IClientsClient ClientsClient { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private ClientForEdit ClientForEditVm { get; set; } = new();
        private UpdateClientCommand UpdateClientCommand { get; set; }

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Clients, "clients"),
                new(Resource.Edit_Client, "#", true)
            });

            System.Console.WriteLine("client id: " + ClientId);

            var httpResponseWrapper = await ClientsClient.GetClient(new GetClientForEditQuery
            {
                Id = ClientId,
            });

            if (httpResponseWrapper.Success)
            {
                System.Console.WriteLine("httpResponseWrapper.Success");
                var successResult = httpResponseWrapper.Response as SuccessResult<ClientForEdit>;
                ClientForEditVm = successResult?.Result;
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
            UpdateClientCommand = new UpdateClientCommand
            {
                Id = ClientForEditVm.Id,
                FullName = ClientForEditVm.FullName,
                PhoneNumber = ClientForEditVm.PhoneNumber,
                Email = ClientForEditVm.Email,
            };
            var httpResponse = await ClientsClient.UpdateClient(UpdateClientCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("clients");
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
