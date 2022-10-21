namespace D2W.WebPortal.Shared;

public partial class GlobalSpinner
{
    #region Protected Properties

    protected bool IsVisible { get; set; }

    #endregion Protected Properties

    #region Private Properties

    [Inject] private SpinnerService SpinnerService { get; set; }

    #endregion Private Properties

    #region Public Methods

    public void ShowSpinner()
    {
        IsVisible = true;
        StateHasChanged();
    }

    public void HideSpinner()
    {
        IsVisible = false;
        StateHasChanged();
    }

    #endregion Public Methods

    #region Protected Methods

    protected override void OnInitialized()
    {
        SpinnerService.OnShow += ShowSpinner;
        SpinnerService.OnHide += HideSpinner;
    }

    #endregion Protected Methods
}