using D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.DesignConcepts
{
    public partial class DesignConcepts : ComponentBase, IAsyncDisposable
    {
        #region Private Properties

        public int ActivePanelIndex { get; set; } = 0;
        [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
        [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
        [Inject] private IDesignConceptsClient DesignConceptsClient { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IJSRuntime Js { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private bool disableOptionButtons = true;

        private string title = "DesignConcepts";
        private string description = "List of clients for interior designers.";
        private string SearchString { get; set; }
        private bool IsHubConnectionClosed { get; set; }
        private DesignConceptsResponse DesignConceptsResponse { get; set; }
        private ServerSideValidator ServerSideValidator { get; set; }
        private GetDesignConceptsQuery GetDesignConceptsQuery { get; set; } = new();
        private HubConnection HubConnection { get; set; }
        private MudTable<DesignConceptItem> Table { get; set; }

        #endregion Private Properties

        #region Public Methods

        public async ValueTask DisposeAsync()
        {

        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Design_Concepts, "#", true)
            });
        }

        #endregion Protected Methods

        #region Private Methods

        private void AddDesignConcept()
        {
            NavigationManager.NavigateTo("AddDesignConcept");
        }

        private void EditDesignConcept(Guid id)
        {
            NavigationManager.NavigateTo($"EditDesignConcept/{id}");
        }

        private void ViewDesignConcept(Guid id)
        {
            NavigationManager.NavigateTo($"ViewDesignConcept/{id}");
        }

        private async Task DeleteDesignConcept(Guid id)
        {
            var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_really_want_to_delete_this_record},
            {"ButtonText", Resource.Delete},
            {"Color", Color.Error}
        };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogModal>(Resource.Delete, parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var httpResponseWrapper = await DesignConceptsClient.DeleteDesignConcept(id);

                if (httpResponseWrapper.Success)
                {
                    var successResult = httpResponseWrapper.Response as SuccessResult<string>;
                    Snackbar.Add(successResult.Result, Severity.Success);
                    await Table.ReloadServerData();
                }
                else
                {
                    var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                    ServerSideValidator.Validate(exceptionResult);
                }
            }
        }

        private void FilterDesignConcepts(string searchString)
        {
            if (DesignConceptsResponse is null)
                return;
            SearchString = searchString;
            Table.ReloadServerData();
        }

        private async Task<TableData<DesignConceptItem>> ServerReload(TableState state)
        {
            GetDesignConceptsQuery.SearchText = SearchString;

            GetDesignConceptsQuery.PageNumber = state.Page + 1;

            GetDesignConceptsQuery.RowsPerPage = state.PageSize;

            GetDesignConceptsQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

            var responseWrapper = await DesignConceptsClient.GetDesignConcepts(GetDesignConceptsQuery);

            var tableData = new TableData<DesignConceptItem>();

            if (responseWrapper.Success)
            {
                var successResult = responseWrapper.Response as SuccessResult<DesignConceptsResponse>;
                if (successResult != null)
                    DesignConceptsResponse = successResult.Result;

                tableData = new TableData<DesignConceptItem>()
                { TotalItems = DesignConceptsResponse.DesignConcepts.TotalRows, Items = DesignConceptsResponse.DesignConcepts.Items };
            }
            else
            {
                var exceptionResult = responseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }

            return tableData;
        }

        #endregion Private Methods
    }
}
