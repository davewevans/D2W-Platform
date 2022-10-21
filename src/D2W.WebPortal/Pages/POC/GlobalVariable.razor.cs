namespace D2W.WebPortal.Pages.POC;

public partial class GlobalVariable
{
    #region Private Properties

    private bool IsTipsOpen { get; set; }

    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }

    [Inject] private IAppStateManager AppStateManager { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Global_Variable, "#", true)
        });

        AppStateManager.AppStateChanged += AppStateChanged;
    }

    #endregion Protected Methods

    #region Private Methods

    private void TipsToggle()
    {
        IsTipsOpen = !IsTipsOpen;
    }

    private async void AppStateChanged(object sender, EventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }

    #endregion Private Methods
}