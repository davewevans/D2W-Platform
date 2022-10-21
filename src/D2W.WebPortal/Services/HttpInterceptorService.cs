namespace D2W.WebPortal.Services;

public class HttpInterceptorService : IDisposable
{
    #region Private Fields

    private readonly HttpClient _httpClient;
    private readonly HttpClientInterceptor _httpClientInterceptor;
    private readonly SpinnerService _spinnerService;
    private readonly NavigationManager _navigationManager;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IAuthenticationService _authenticationService;

    #endregion Private Fields

    #region Public Constructors

    public HttpInterceptorService(IHttpClientFactory httpClientFactory,
                                  HttpClientInterceptor httpClientInterceptor,
                                  SpinnerService spinnerService,
                                  NavigationManager navigationManager,
                                  IRefreshTokenService refreshTokenService,
                                  IAuthenticationService authenticationService)
    {
        _httpClient = httpClientFactory.CreateClient("HttpInterceptorService");
        _httpClientInterceptor = httpClientInterceptor;
        _spinnerService = spinnerService;
        _navigationManager = navigationManager;
        _refreshTokenService = refreshTokenService;
        _authenticationService = authenticationService;
        _httpClientInterceptor.BeforeSendAsync += async (s, e) => await HttpClientInterceptor_BeforeSendAsync(s, e);
        _httpClientInterceptor.AfterSendAsync += async (s, e) => await HttpClientInterceptor_AfterSendAsync(s, e);
    }

    #endregion Public Constructors

    #region Public Methods

    public void Dispose()
    {
        _httpClientInterceptor.BeforeSendAsync -= HttpClientInterceptor_BeforeSendAsync;
        _httpClientInterceptor.AfterSendAsync -= HttpClientInterceptor_AfterSendAsync;
        _httpClient.Dispose();
    }

    #endregion Public Methods

    #region Private Methods

    private async Task HttpClientInterceptor_BeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        _spinnerService.Show();

        var subDomain = _navigationManager.GetSubDomain();

        if (subDomain != null)
            e.Request.Headers.Add("X-Tenant", subDomain);

        if (e.Request.Headers.Authorization != null)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("/api/account/"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    private async Task HttpClientInterceptor_AfterSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        _spinnerService.Hide();
        if (e.Response is { StatusCode: HttpStatusCode.Unauthorized })
            await _authenticationService.Logout();

        await Task.CompletedTask;
    }

    #endregion Private Methods
}