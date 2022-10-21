namespace D2W.WebPortal.Pages.Users;

public partial class UserRolesForAdd
{
    #region Public Properties

    [Parameter] public EventCallback<List<RoleItem>> OnAssignedRolesChanged { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }

    private string SearchText { get; set; }
    private List<RoleItem> RoleItems { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private bool FilterRoles(RoleItem roleItem)
    {
        return string.IsNullOrWhiteSpace(SearchText) || roleItem.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    private async Task RemoveRole(RoleItem item)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_want_to_remove_this_record},
            {"ButtonText", Resource.Remove},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Remove, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            RoleItems.Remove(item);
        }
    }

    private async Task ShowRolesLookupDialog()
    {
        var parameters = new DialogParameters { { nameof(RolesLookupDialog.SelectedUserRoles), RoleItems.ToList() } };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

        var dialog = DialogService.Show<RolesLookupDialog>(Resource.Assign_Roles, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            SearchText = null;
            RoleItems = (List<RoleItem>)result.Data;
            await OnAssignedRolesChanged.InvokeAsync(RoleItems);
        }
    }

    #endregion Private Methods
}