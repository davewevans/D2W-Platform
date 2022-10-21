namespace D2W.HostApp.Shared;

public partial class MainLayout
{
    #region Public Fields

    public bool IsDarkMode = false;

    #endregion Public Fields

    #region Private Fields

    private MudTheme _currentTheme = new();

    private MudTheme _defaultTheme = new()
    {
        Palette = new Palette()
        {
            Black = "#272c34"
        }
    };

    private MudTheme _darkTheme = new()
    {
        Palette = new Palette()
        {
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
            TextDisabled = "rgba(255,255,255, 0.2)"
        }
    };

    #endregion Private Fields

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        _currentTheme = _defaultTheme;
        await InvokeAsync(StateHasChanged);
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