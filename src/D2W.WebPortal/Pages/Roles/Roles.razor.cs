namespace D2W.WebPortal.Pages.Roles;

public partial class Roles
{
    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IRolesClient RolesClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string SearchString { get; set; }
    private MudTable<RoleItem> Table { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private GetRolesQuery GetRolesQuery { get; set; } = new();
    private RolesResponse RolesResponse { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Roles, "#",true)
        });
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task<TableData<RoleItem>> ServerReload(TableState state)
    {
        GetRolesQuery.SearchText = SearchString;

        GetRolesQuery.PageNumber = state.Page + 1;

        GetRolesQuery.RowsPerPage = state.PageSize;

        GetRolesQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var responseWrapper = await RolesClient.GetRoles(GetRolesQuery);

        var tableData = new TableData<RoleItem>();

        if (responseWrapper.Success)
        {
            var successResult = responseWrapper.Response as SuccessResult<RolesResponse>;
            RolesResponse = successResult.Result;
            tableData = new TableData<RoleItem>() { TotalItems = RolesResponse.Roles.TotalRows, Items = RolesResponse.Roles.Items };
        }
        else
        {
            var exceptionResult = responseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
        return tableData;
    }

    private void FilterRoles(string searchString)
    {
        if (RolesResponse is null)
            return;
        SearchString = searchString;
        Table.ReloadServerData();
    }

    private void AddRole()
    {
        NavigationManager.NavigateTo("roles/addRole");
    }

    private void EditRole(string id)
    {
        NavigationManager.NavigateTo($"roles/editRole/{id}");
    }

    private async Task DeleteRole(string id)
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
            var httpResponseWrapper = await RolesClient.DeleteRole(id);

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