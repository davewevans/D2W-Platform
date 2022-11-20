using D2W.WebPortal.Features.Clients.Queries.GetClientForEdit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Clients
{
    public partial class ViewClient : ComponentBase
    {
        #region Public Properties

        [Parameter] public string ClientId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IClientsClient ClientsClient { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private ClientForEdit ClientForEditVm { get; set; } = new();

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Clients, "/Clients"),
            new(Resource.View_Client, "#", true)
        });

            var httpResponseWrapper = await ClientsClient.GetClient(new GetClientForEditQuery
            {
                Id = ClientId,
            });

            if (httpResponseWrapper.Success)
            {
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
    }
}
