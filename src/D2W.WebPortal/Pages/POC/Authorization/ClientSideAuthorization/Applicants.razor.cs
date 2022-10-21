namespace D2W.WebPortal.Pages.POC.Authorization.ClientSideAuthorization;

public partial class Applicants
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IApplicantsClient ApplicantsClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string SearchString { get; set; }
    private MudTable<ApplicantItem> Table { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private GetApplicantsQuery GetApplicantsQuery { get; set; } = new();
    private ApplicantsResponse ApplicantsResponse { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Proof_of_Concepts, "#", true),
            new(Resource.Authorization, "#", true),
            new(Resource.Client_Side_Authorization, "#", true),
            new(Resource.Applicants, "#",true)
        });
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task<TableData<ApplicantItem>> ServerReload(TableState state)
    {
        GetApplicantsQuery.SearchText = SearchString;

        GetApplicantsQuery.PageNumber = state.Page + 1;

        GetApplicantsQuery.RowsPerPage = state.PageSize;

        GetApplicantsQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var responseWrapper = await ApplicantsClient.GetApplicants(GetApplicantsQuery);

        var tableData = new TableData<ApplicantItem>();

        if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<ApplicantsResponse>;
            ApplicantsResponse = successResult.Result;
            tableData = new TableData<ApplicantItem>() { TotalItems = ApplicantsResponse.Applicants.TotalRows, Items = ApplicantsResponse.Applicants.Items };
        }
        else
        {
            var exceptionResult = responseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
        return tableData;
    }

    private void FilterApplicants(string searchString)
    {
        if (ApplicantsResponse is null)
            return;
        SearchString = searchString;
        Table.ReloadServerData();
    }

    private void AddApplicant()
    {
        NavigationManager.NavigateTo("poc/authorization/clientSideAuthorization/AddApplicant");
    }

    private void ViewApplicant(string id)
    {
        NavigationManager.NavigateTo($"poc/authorization/clientSideAuthorization/ViewApplicant/{id}");
    }

    private void EditApplicant(string id)
    {
        NavigationManager.NavigateTo($"poc/authorization/clientSideAuthorization/EditApplicant/{id}");
    }

    private async Task DeleteApplicant(string id)
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
            var httpResponseWrapper = await ApplicantsClient.DeleteApplicant(id);

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

    #endregion Private Methods
}