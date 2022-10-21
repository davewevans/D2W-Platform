namespace D2W.WebPortal.Shared;

public partial class HeaderButtons
{
    #region Private Properties

    [Inject] private INavigationService NavigationService { get; set; }
    [Inject] private IAppStateManager AppStateManager { get; set; }

    #endregion Private Properties

    #region Private Methods

    private void OnSoundToggledChanged(bool toggled)
    {
        // Because variable is not two-way bound, we need to update it.
        AppStateManager.Visible = toggled;
    }

    #endregion Private Methods
}