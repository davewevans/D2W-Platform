namespace D2W.WebPortal.Shared;

public partial class MainLayout
{
    #region Public Properties

    public bool DrawerOpen { get; set; } = true;

    public bool IsRightToLeft { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private HttpInterceptorService HttpInterceptorService { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    #endregion Private Properties

    #region Protected Methods

    private MudThemeProvider _mudThemeProvider;
    private bool IsDarkMode { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsDarkMode = await _mudThemeProvider.GetSystemPreference();
            StateHasChanged();
        }
    }

    protected override void OnInitialized()
    {
        var culture = CultureInfo.CurrentCulture;
        IsRightToLeft = culture.TextInfo.IsRightToLeft;
        NavigationManager.LocationChanged += (obj, nav) => { StateHasChanged(); }; // To refresh the breadcrumb component
        Snackbar.Configuration.PositionClass = !IsRightToLeft ? Defaults.Classes.Position.TopRight : Defaults.Classes.Position.TopLeft;
    }

    #endregion Protected Methods

    #region Private Methods

    private void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }

    private void DarkModeToggle()
    {
        IsDarkMode = !IsDarkMode;
    }

    #endregion Private Methods
}