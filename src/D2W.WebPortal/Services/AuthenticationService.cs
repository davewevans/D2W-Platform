namespace D2W.WebPortal.Services;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields

    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    #endregion Private Fields

    #region Public Constructors

    public AuthenticationService(HttpClient httpClient,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task Login(AuthResponse authResponse)
    {
        System.Console.WriteLine("Login invoked");

        await Logout();

        await _localStorageService.SetItemAsync(TokenType.AccessToken, authResponse.AccessToken);

        await _localStorageService.SetItemAsync(TokenType.RefreshToken, authResponse.RefreshToken);

        await StoreTenantNameFromClaim();

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.AccessToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.AccessToken);
    }

    public async Task ReAuthenticate(AuthResponse authResponse)
    {
        System.Console.WriteLine("ReAuthenticate invoked");

        await CleanUp();

        await _localStorageService.SetItemAsync(TokenType.AccessToken, authResponse.AccessToken);

        await _localStorageService.SetItemAsync(TokenType.RefreshToken, authResponse.RefreshToken);

        await StoreTenantNameFromClaim();

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.AccessToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.AccessToken);
    }

    public async Task Logout()
    {
        var authState = await ((AuthStateProvider)_authStateProvider).GetAuthenticationStateAsync();

        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
        {
            await _localStorageService.RemoveItemAsync(TokenType.AccessToken);
            await _localStorageService.RemoveItemAsync(TokenType.RefreshToken);
            await _localStorageService.RemoveItemAsync(Constants.TenantNameStorageKey);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public async Task StoreTenantName(string tenantName)
    {
        System.Console.WriteLine("StoreTenantName invoked");
        await _localStorageService.SetItemAsync(Constants.TenantNameStorageKey, tenantName);
    }

    public async Task RemoveTenantName()
    {
        await _localStorageService.RemoveItemAsync(Constants.TenantNameStorageKey);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task CleanUp()
    {
        await _localStorageService.RemoveItemAsync(TokenType.AccessToken);

        await _localStorageService.RemoveItemAsync(TokenType.RefreshToken);

        await _localStorageService.RemoveItemAsync(Constants.TenantNameStorageKey);

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private async Task StoreTenantNameFromClaim()
    {

        System.Console.WriteLine("StoreTenantNameFromClaim invoked");

        var authenticationState = await _authStateProvider.GetAuthenticationStateAsync();
        var tenantNameClaim = authenticationState.User.Claims.FirstOrDefault(x => x.Type.Equals("TenantName"));
        string tenantName = tenantNameClaim is not null ? tenantNameClaim.Value : string.Empty;

        await _localStorageService.SetItemAsync(Constants.TenantNameStorageKey, tenantName);
    }

    #endregion Private Methods
}