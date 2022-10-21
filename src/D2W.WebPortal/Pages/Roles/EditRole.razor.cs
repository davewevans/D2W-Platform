namespace D2W.WebPortal.Pages.Roles;

public partial class EditRole
{
    #region Public Properties

    [Parameter] public string RoleId { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IRolesClient RolesClient { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private bool ShouldReload { get; set; } = true;
    private bool LoadingOnDemand { get; set; }
    private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
    private HashSet<PermissionItem> PermissionItems { get; set; } = new();
    private RoleForEdit RoleForEditVm { get; set; } = new();
    private HashSet<PermissionItem> SelectedPermissionItemsForEdit { get; set; }
    private HashSet<PermissionItem> SelectedPermissionItemsForView { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private UpdateRoleCommand UpdateRoleCommand { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Roles, "/roles"),
            new(Resource.Edit_Role, "#", true)
        });

        await GetRole();

        await InitializeTree(LoadingOnDemand);
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task GetRole()
    {
        var httpResponseWrapper = await RolesClient.GetRole(new GetRoleForEditQuery
        {
            Id = RoleId
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<RoleForEdit>;
            RoleForEditVm = successResult?.Result;
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private async Task InitializeTree(bool loadingOnDemand)
    {
        SelectedPermissionItemsForView = new HashSet<PermissionItem>();
        SelectedPermissionItemsForEdit = new HashSet<PermissionItem>();
        PermissionItems = new HashSet<PermissionItem>();

        LoadingOnDemand = loadingOnDemand;

        var httpResponseWrapper = await RolesClient.GetRolePermissions(new GetRolePermissionsForEditQuery()
        {
            PermissionId = null,
            RoleId = RoleId,
            LoadingOnDemand = loadingOnDemand
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<RolePermissionsForEdit>;
            SelectedPermissionItemsForView = successResult?.Result.SelectedPermissions.ToHashSet();
            SelectedPermissionItemsForEdit = successResult?.Result.SelectedPermissions.ToHashSet();
            PermissionItems = successResult?.Result.RequestedPermissions.ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private async Task<HashSet<PermissionItem>> LoadServerData(PermissionItem parentNode)
    {
        var httpResponseWrapper = await RolesClient.GetRolePermissions(new GetRolePermissionsForEditQuery
        {
            RoleId = RoleId,
            LoadingOnDemand = true,
            PermissionId = parentNode.Id
        });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<RolePermissionsForEdit>;
            SelectedPermissionItemsForView = successResult?.Result.SelectedPermissions.ToHashSet();
            parentNode.Permissions = successResult?.Result.RequestedPermissions.ToList();
            return (parentNode.Permissions ?? throw new InvalidOperationException()).ToHashSet();
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }

        return new HashSet<PermissionItem>();
    }

    private async Task SubmitForm()
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Are_you_sure_you_want_to_save_role},
            {"ButtonText", Resource.Yes},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>("Confirm", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            UpdateRoleCommand = new UpdateRoleCommand
            {
                Id = RoleForEditVm.Id,
                Name = RoleForEditVm.Name,
                IsDefault = RoleForEditVm.IsDefault,
                SelectedPermissionIds = LoadingOnDemand
                    ? SelectedPermissionItemsForEdit.Select(p => p.Id).ToList()
                    : SelectedPermissionItemsForView.Select(p => p.Id).ToList()
            };
            var httpResponse = await RolesClient.UpdateRole(UpdateRoleCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("roles");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
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

    #endregion Private Methods
}