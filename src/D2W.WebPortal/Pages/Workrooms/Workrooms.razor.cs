using D2W.WebPortal.Features.Workrooms.Queries.GetWorkrooms;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Workrooms
{
    public partial class Workrooms : ComponentBase, IAsyncDisposable
    {
        #region Private Properties

        public int ActivePanelIndex { get; set; } = 0;
        [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
        [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
        [Inject] private IWorkroomsClient WorkroomsClient { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IJSRuntime Js { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private bool disableOptionButtons = true;

        private string title = "Workrooms";
        private string description = "List of workrooms for interior designers.";
        private string SearchString { get; set; }
        private bool IsHubConnectionClosed { get; set; }
        private WorkroomsResponse WorkroomsResponse { get; set; }
        private ServerSideValidator ServerSideValidator { get; set; }
        private GetWorkroomsQuery GetWorkroomsQuery { get; set; } = new();
        private HubConnection HubConnection { get; set; }
        private MudTable<WorkroomItem> Table { get; set; }

        #endregion Private Properties

        #region Public Methods

        public async ValueTask DisposeAsync()
        {
            if (HubConnection is not null && HubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    await HubConnection.StopAsync();
                }
                finally
                {
                    await HubConnection.DisposeAsync();
                    Snackbar.Add("Reporting Hub is closed.", Severity.Error);
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Workrooms, "#", true)
        });

            // await StartHubConnection();

            // HubConnection.On("NotifyReportIssuer", (Func<FileMetaData, ReportStatus, Task>)(async (fileMetaData, reportStatus) =>
            // {
            //     switch (reportStatus)
            //     {
            //         case ReportStatus.Pending:
            //             Snackbar.Add(Resource.Your_report_is_being_initiated, Severity.Info);
            //             break;

            //         case ReportStatus.InProgress:
            //             Snackbar.Add(Resource.Your_report_is_being_generated,
            //         Severity.Warning);
            //             break;

            //         case ReportStatus.Completed:
            //             Snackbar.Add(
            //         string.Format(Resource.Your_report_0_is_ready_to_download, fileMetaData.FileName),
            //         Severity.Success);
            //             await ShowDownloadFileDialogue(fileMetaData, reportStatus);
            //             break;

            //         case ReportStatus.Failed:
            //             Snackbar.Add(Resource.Your_report_generation_has_failed,
            //         Severity.Error);
            //             break;

            //         default:
            //             throw new ArgumentOutOfRangeException(nameof(reportStatus), reportStatus, null);
            //     }
            // }));
        }

        #endregion Protected Methods

        #region Private Methods

        public async Task CloseHubConnection()
        {
            ActivePanelIndex = 0;

            if (HubConnection is null)
                return;

            switch (HubConnection.State)
            {
                case HubConnectionState.Connected:
                    try
                    {
                        await HubConnection.StopAsync();
                    }
                    finally
                    {
                        await HubConnection.DisposeAsync();
                        IsHubConnectionClosed = true;
                        Snackbar.Add("Reporting Hub is closed.", Severity.Error);
                    }
                    break;

                case HubConnectionState.Disconnected:
                    Snackbar.Add("Reporting Hub is already closed.", Severity.Success);
                    break;
            }
        }

        private void AddWorkroom()
        {
            NavigationManager.NavigateTo("AddWorkroom");
        }

        private void EditWorkroom(string id)
        {
            NavigationManager.NavigateTo($"EditWorkroom/{id}");
        }

        private void ViewWorkroom(string id)
        {
            NavigationManager.NavigateTo($"ViewWorkroom/{id}");
        }

        private async Task DeleteWorkroom(string id)
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
                var httpResponseWrapper = await WorkroomsClient.DeleteWorkroom(id);

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

        private void FilterWorkrooms(string searchString)
        {
            if (WorkroomsResponse is null)
                return;
            SearchString = searchString;
            Table.ReloadServerData();
        }

        // private async Task ImmediateExportApplicantToPdf()
        // {
        //     var parameters = new DialogParameters
        //         {
        //             {"ContentText", Resource.Exporting_data_may_take_a_while},
        //             {"ButtonText", Resource.ExportAsPdfImmediate},
        //             {"Color", Color.Error}
        //         };

        //     var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        //     var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

        //     var result = await dialog.Result;

        //     if (!result.Cancelled)
        //     {
        //         var httpResponseWrapper = await WorkroomsWorkroom.ExportAsPdf(new ExportApplicantsQuery
        //         {
        //             SearchText = GetWorkroomsQuery.SearchText,
        //             SortBy = GetWorkroomsQuery.SortBy,
        //             IsImmediate = true
        //         });

        //         if (httpResponseWrapper.Success)
        //         {
        //             if (httpResponseWrapper.Response is SuccessResult<ExportApplicantsResponse> successResult)
        //             {
        //                 Snackbar.Add(successResult.Result.FileUrl, Severity.Success);
        //                 await Js.InvokeVoidAsync("triggerFileDownload", successResult.Result.FileName, successResult.Result.FileUrl);
        //             }
        //         }
        //         else
        //         {
        //             var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
        //             ServerSideValidator.Validate(exceptionResult);
        //         }
        //     }
        // }

        // private async Task PostponedExportApplicantToPdf()
        // {
        //     ActivePanelIndex = 0;

        //     var parameters = new DialogParameters
        //     {
        //         {"ContentText", Resource.Exporting_data_may_take_a_while},
        //         {"ButtonText", Resource.ExportAsPdfInBackground},
        //         {"Color", Color.Error}
        //     };

        //     if (HubConnection.State == HubConnectionState.Connected)
        //     {
        //         var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        //         var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

        //         var result = await dialog.Result;

        //         if (!result.Cancelled)
        //         {
        //             await HubConnection.SendAsync("ExportApplicantToPdf", new ExportApplicantsQuery
        //             {
        //                 SearchText = GetWorkroomsQuery.SearchText,
        //                 SortBy = GetWorkroomsQuery.SortBy,
        //                 IsImmediate = false
        //             });

        //             ActivePanelIndex = 1;
        //         }
        //     }
        //     else
        //     {
        //         Snackbar.Add($@"Reporting Hub is not active.", Severity.Warning);
        //     }
        // }

        private async Task<TableData<WorkroomItem>> ServerReload(TableState state)
        {
            GetWorkroomsQuery.SearchText = SearchString;

            GetWorkroomsQuery.PageNumber = state.Page + 1;

            GetWorkroomsQuery.RowsPerPage = state.PageSize;

            GetWorkroomsQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

            var responseWrapper = await WorkroomsClient.GetWorkrooms(GetWorkroomsQuery);

            var tableData = new TableData<WorkroomItem>();

            if (responseWrapper.Success)
            {
                var successResult = responseWrapper.Response as SuccessResult<WorkroomsResponse>;
                if (successResult != null)
                    WorkroomsResponse = successResult.Result;

                tableData = new TableData<WorkroomItem>()
                { TotalItems = WorkroomsResponse.Workrooms.TotalRows, Items = WorkroomsResponse.Workrooms.Items };
            }
            else
            {
                var exceptionResult = responseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }

            return tableData;
        }

        private async Task ShowDownloadFileDialogue(FileMetaData fileMetaData, ReportStatus reportStatus)
        {
            var parameters = new DialogParameters
        {
            {"ContentText", Resource.Your_report_is_ready_to_download},
            {"ButtonText", Resource.Download},
            {"Color", Color.Error}
        };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<DialogModal>(Resource.Export, parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
                await Js.InvokeVoidAsync("triggerFileDownload", fileMetaData.FileName, fileMetaData.FileUri);
        }

        private async Task StartHubConnection()
        {
            if (HubConnection is null || HubConnection.State == HubConnectionState.Disconnected)
            {
                Snackbar.Add("Reporting Hub is being initialized.", Severity.Info);

                var subDomain = NavigationManager.GetSubDomain();

                HubConnection = new HubConnectionBuilder()
                    .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DataExportHub?X-Tenant={subDomain}&Accept-Language={CultureInfo.CurrentCulture}",
                        options =>
                        {
                            //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
                            //options.Headers.Add("Accept-Language", culture); //Doesn't Work
                            options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
                        }).Build();

                try
                {
                    await HubConnection.StartAsync();
                }
                catch (Exception e)
                {
                    Snackbar.Add($@"Unable to connect to the reporting hub due to an error: {e.Message}", Severity.Error);
                }

                if (HubConnection.State == HubConnectionState.Connected)
                {
                    IsHubConnectionClosed = false;
                    Snackbar.Add("Reporting Hub is now connected.", Severity.Success);
                }

                HubConnection.Closed += OnHubConnectionClosed;
            }
        }

        private async Task OnHubConnectionToggledChanged(bool toggled)
        {
            if (toggled)
            {
                await CloseHubConnection();
            }
            else
            {
                await StartHubConnection();
            }
        }

        private Task OnHubConnectionClosed(Exception exception)
        {
            switch (exception)
            {
                case null:
                    Snackbar.Add("Reporting Hub is closed.", Severity.Error);
                    break;

                default:
                    Snackbar.Add($@"Reporting Hub connection closed due to an error: {exception.Message}", Severity.Error);
                    IsHubConnectionClosed = true;
                    break;
            }

            return Task.CompletedTask;
        }

        #endregion Private Methods
    }
}
