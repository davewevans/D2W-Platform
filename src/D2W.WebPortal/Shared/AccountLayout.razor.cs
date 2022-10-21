namespace D2W.WebPortal.Shared;

public partial class AccountLayout
{
    #region Private Fields

    private readonly MudTheme _defaultTheme = new();

    private readonly MudTheme _darkTheme = new()
    {
        Palette = new Palette()
        {
            Primary = "#776be7",
            Black = "#27272f",
            Background = "#32333d",
            BackgroundGrey = "#27272f",
            Surface = "#373740",
            DrawerBackground = "#27272f",
            DrawerText = "rgba(255,255,255, 0.50)",
            DrawerIcon = "rgba(255,255,255, 0.50)",
            AppbarBackground = "#27272f",
            AppbarText = "rgba(255,255,255, 0.70)",
            TextPrimary = "rgba(255,255,255, 0.70)",
            TextSecondary = "rgba(255,255,255, 0.50)",
            ActionDefault = "#adadb1",
            ActionDisabled = "rgba(255,255,255, 0.26)",
            ActionDisabledBackground = "rgba(255,255,255, 0.12)",
            Divider = "rgba(255,255,255, 0.12)",
            DividerLight = "rgba(255,255,255, 0.06)",
            TableLines = "rgba(255,255,255, 0.12)",
            LinesDefault = "rgba(255,255,255, 0.12)",
            LinesInputs = "rgba(255,255,255, 0.3)",
            TextDisabled = "rgba(255,255,255, 0.2)",
            Info = "#3299ff",
            Success = "#0bba83",
            Warning = "#ffa800",
            Error = "#f64e62",
            Dark = "#27272f",
        }
    };

    private MudTheme _currentTheme = new();

    #endregion Private Fields

    #region Public Properties

    //public bool IsDarkMode { get; set; };
    public bool IsDarkMode { get; set; } = false;

    public bool IsRightToLeft { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private HttpInterceptorService HttpInterceptorService { get; set; } //DO NOT REMOVE.
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        _currentTheme = _defaultTheme;
        //_currentTheme = _darkTheme;
        var culture = CultureInfo.CurrentCulture;
        IsRightToLeft = culture.TextInfo.IsRightToLeft;
        NavigationManager.LocationChanged += (obj, nav) => { StateHasChanged(); }; // To refresh the breadcrumb component
    }

    #endregion Protected Methods

    #region Private Methods

    private void DarkMode()
    {
        if (_currentTheme == _defaultTheme)
        {
            _currentTheme = _darkTheme;
            IsDarkMode = true;
        }
        else
        {
            _currentTheme = _defaultTheme;
            IsDarkMode = false;
        }
    }

    #endregion Private Methods
}