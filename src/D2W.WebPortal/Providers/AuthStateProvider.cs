namespace D2W.WebPortal.Providers;

public class AuthStateProvider : AuthenticationStateProvider
{
    #region Private Fields

    private readonly HttpClient _httpClient;
    private readonly AuthenticationState _anonymous;
    private readonly IAccessTokenProvider _accessTokenProvider;

    #endregion Private Fields

    #region Public Constructors

    public AuthStateProvider(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
    {
        _httpClient = httpClient;
        _accessTokenProvider = accessTokenProvider;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    #endregion Public Constructors

    #region Public Methods

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _accessTokenProvider.TryGetAccessToken();

        if (string.IsNullOrWhiteSpace(token))
            return _anonymous;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);

        NotifyAuthenticationStateChanged(authState);
    }

    #endregion Public Methods
}