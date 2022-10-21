namespace D2W.WebPortal.Pages.Users;

public partial class PermissionsLookupDialog
{
    #region Public Properties

    [Parameter] public string UserId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IUsersClient UsersClient { get; set; }
    [Inject] private IPermissionsClient PermissionsClient { get; set; }

    private bool LoadingOnDemand { get; set; }
    private bool ShouldReload { get; set; } = true;
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private GrantOrRevokeUserPermissionsCommand GrantOrRevokePermissionsCommand { get; set; } = new();
    private HashSet<PermissionItem> PermissionItems { get; set; } = new();
    private HashSet<PermissionItem> SelectedPermissionItemsForView { get; set; }
    private HashSet<PermissionItem> SelectedPermissionItemsForEdit { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        await InitializeTree(LoadingOnDemand);
    }

    protected override bool ShouldRender()
    {
        if (ShouldReload)
        {
            return true;
        }
        return false;
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task InitializeTree(bool loadingOnDemand)
    {
        SelectedPermissionItemsForView = new HashSet<PermissionItem>();
        SelectedPermissionItemsForEdit = new HashSet<PermissionItem>();
        PermissionItems = new HashSet<PermissionItem>();

        LoadingOnDemand = loadingOnDemand;

        var httpResponseWrapper = await UsersClient.GetUserPermissions(new GetUserPermissionsQuery
        {
            UserId = UserId,
            LoadingOnDemand = LoadingOnDemand
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<UserPermissionsResponse>;
            SelectedPermissionItemsForView = successResult?.Result.SelectedPermissions.ToHashSet();
            SelectedPermissionItemsForEdit = successResult?.Result.SelectedPermissions.ToHashSet();
            PermissionItems = successResult?.Result.RequestedPermissions.ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
    }

    private async Task<HashSet<PermissionItem>> LoadServerData(PermissionItem parentNode)
    {
        var httpResponseWrapper = await PermissionsClient.GetPermissions(new GetPermissionsQuery
        {
            Id = parentNode.Id,
            LoadingOnDemand = LoadingOnDemand
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<PermissionsResponse>;
            parentNode.Permissions = successResult?.Result.Permissions.ToList() ?? new List<PermissionItem>();
            parentNode.Permissions.ForEach(p => p.IsChecked = SelectedPermissionItemsForView.Any(sp => p.Id == sp.Id));
            return (parentNode.Permissions ?? throw new InvalidOperationException()).ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
        return new HashSet<PermissionItem>();
    }

    private void UpdateSelectedPermissions(bool isChecked, PermissionItem permission)
    {
        ShouldReload = false;
        var permissionExist = SelectedPermissionItemsForEdit.Any(sp => sp.Id == permission.Id);
        if (isChecked)
        {
            if (!permissionExist)
                SelectedPermissionItemsForEdit.Add(permission);
        }
        else
        {
            if (permissionExist)
                SelectedPermissionItemsForEdit.RemoveWhere(x => x.Id == permission.Id);
        }

        ShouldReload = true;
    }

    private async Task SubmitForm()
    {
        GrantOrRevokePermissionsCommand.UserId = UserId;

        GrantOrRevokePermissionsCommand.SelectedPermissionIds = LoadingOnDemand
            ? SelectedPermissionItemsForEdit.Select(p => p.Id).ToList()
            : SelectedPermissionItemsForView.Select(p => p.Id).ToList();

        var httpResponseWrapper = await UsersClient.GrantOrRevokeUserPermissions(GrantOrRevokePermissionsCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<string>;
            MudDialog.Close();
            Snackbar.Add(successResult.Result, Severity.Success);
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
        }
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    #endregion Private Methods
}