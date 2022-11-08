using D2W.WebPortal.Theme;

namespace D2W.WebPortal.Shared;

public partial class AccountLayout
{
    #region Private Fields

        private readonly MudTheme _defaultTheme = new AppLightModeTheme();

        private readonly MudTheme _darkTheme = new AppDarkModeTheme();

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

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = _defaultTheme;
            //_currentTheme = _darkTheme;
            IsRightToLeft = !string.IsNullOrWhiteSpace(await LocalStorage.GetItemAsStringAsync("IsRightToLeft")) && Convert.ToBoolean(await LocalStorage.GetItemAsStringAsync("IsRightToLeft"));
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
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