namespace D2W.WebPortal.Pages.Roles;

public partial class AddRole
{
    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IRolesClient RolesClient { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IPermissionsClient PermissionsClient { get; set; }

    private bool LoadingOnDemand { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private HashSet<PermissionItem> PermissionItems { get; set; } = new();
    private HashSet<PermissionItem> SelectedPermissionItems { get; set; }
    private CreateRoleCommand CreateRoleCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Roles, "/roles"),
            new(Resource.Add_Role, "#", true)
        });

        await InitializeTree(LoadingOnDemand);
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task InitializeTree(bool loadingOnDemand)
    {
        LoadingOnDemand = loadingOnDemand;

        var httpResponseWrapper = await PermissionsClient.GetPermissions(new GetPermissionsQuery
        {
            Id = null,
            LoadingOnDemand = LoadingOnDemand
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<PermissionsResponse>;
            PermissionItems = successResult?.Result?.Permissions.ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private async Task<HashSet<PermissionItem>> LoadServerData(PermissionItem parentNode)
    {
        var httpResponseWrapper = await PermissionsClient.GetPermissions(new GetPermissionsQuery { Id = parentNode.Id, LoadingOnDemand = true });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<PermissionsResponse>;
            parentNode.Permissions = successResult?.Result?.Permissions;
            return (parentNode.Permissions ?? throw new InvalidOperationException()).ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            EditContextServerSideValidator.Validate(exceptionResult);
            ServerSideValidator.Validate(exceptionResult);
        }

        return new HashSet<PermissionItem>();
    }

    private async Task SubmitForm()
    {
        CreateRoleCommand.SelectedPermissionIds = SelectedPermissionItems.Select(p => p.Id).ToList();

        var httpResponseWrapper = await RolesClient.CreateRole(CreateRoleCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateRoleResponse>;
            Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
            NavigationManager.NavigateTo("/roles");
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