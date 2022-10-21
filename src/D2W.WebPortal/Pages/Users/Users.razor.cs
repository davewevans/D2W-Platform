namespace D2W.WebPortal.Pages.Users;

public partial class Users
{
    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IUsersClient UsersClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    private string SearchString { get; set; }
    private MudTable<UserItem> Table { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private List<RoleItem> RoleItem { get; set; } = new();
    private GetUsersQuery GetUsersQuery { get; set; } = new();
    private UsersResponse UsersResponse { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Users, "#",true)
        });
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task<TableData<UserItem>> ServerReload(TableState state)
    {
        GetUsersQuery.SearchText = SearchString;

        GetUsersQuery.SelectedRoleIds = RoleItem.Select(r => r.Id).ToList();

        GetUsersQuery.PageNumber = state.Page + 1;

        GetUsersQuery.RowsPerPage = state.PageSize;

        GetUsersQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var httpResponseWrapper = await UsersClient.GetUsers(GetUsersQuery);
        var tableData = new TableData<UserItem>();
        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<UsersResponse>;
            UsersResponse = successResult.Result;
            tableData = new TableData<UserItem>() { TotalItems = UsersResponse.Users.TotalRows, Items = UsersResponse.Users.Items };
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
        return tableData;
    }

    private void FilterUsers(string searchString)
    {
        if (UsersResponse is null)
            return;
        SearchString = searchString;
        Table.ReloadServerData();
    }

    private async Task ShowRolesLookup()
    {
        var parameters = new DialogParameters { { nameof(RolesLookupDialog.SelectedUserRoles), RoleItem.ToList() } };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

        var dialog = DialogService.Show<RolesLookupDialog>(Resource.Assign_Roles, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            RoleItem = (List<RoleItem>)result.Data;
            FilterUsers(SearchString);
        }
    }


    private void AddUser()
    {
        NavigationManager.NavigateTo("users/addUser");
    }

    private void EditUser(string id)
    {
        NavigationManager.NavigateTo($"users/editUser/{id}");
    }

    private void ShowPermissionsLookupDialog(string userId)
    {
        var parameters = new DialogParameters
        {
            { nameof(PermissionsLookupDialog.UserId), userId }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

        DialogService.Show<PermissionsLookupDialog>(Resource.Assign_Permissions, parameters, options);
    }

    private async Task DeleteUser(string id)
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
            var httpResponseWrapper = await UsersClient.DeleteUser(id);

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