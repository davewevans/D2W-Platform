namespace D2W.Infrastructure.Services;

public class ConfigReaderService : IConfigReaderService
{
    #region Private Fields

    private readonly AppOptions _appOptionsSnapshot;
    private readonly JwtOptions _jwtOptionsSnapshot;
    private readonly SmtpOptions _smtpOptionsSnapshot;
    private readonly SmsOptions _smsOptionsSnapshot;
    private readonly ClientAppOptions _clientAppOptionsSnapshot;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion Private Fields

    #region Public Constructors

    public ConfigReaderService(IOptionsSnapshot<AppOptions> appOptionsSnapshot,
                               IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot,
                               IOptionsSnapshot<ClientAppOptions> clientAppOptionsSnapshot,
                               IOptionsSnapshot<SmtpOptions> smtpOptionSnapshot,
                               IHttpContextAccessor httpContextAccessor,
                               IOptionsSnapshot<SmsOptions> smsOptionsSnapshot)
    {
        _appOptionsSnapshot = appOptionsSnapshot.Value;
        _jwtOptionsSnapshot = jwtOptionsSnapshot.Value;
        _clientAppOptionsSnapshot = clientAppOptionsSnapshot.Value;
        _smtpOptionsSnapshot = smtpOptionSnapshot.Value;
        _httpContextAccessor = httpContextAccessor;
        _smsOptionsSnapshot = smsOptionsSnapshot.Value;
    }

    #endregion Public Constructors

    #region Public Methods

    public AppUserOptions GetAppUserOptions()
    {
        return _appOptionsSnapshot.AppIdentityOptions.AppUserOptions;
    }

    public AppPasswordOptions GetAppPasswordOptions()
    {
        return _appOptionsSnapshot.AppIdentityOptions.AppPasswordOptions;
    }

    public AppLockoutOptions GetAppLockoutOptions()
    {
        return _appOptionsSnapshot.AppIdentityOptions.AppLockoutOptions;
    }

    public AppSignInOptions GetAppSignInOptions()
    {
        return _appOptionsSnapshot.AppIdentityOptions.AppSignInOptions;
    }

    public AppTokenOptions GetAppTokenOptions()
    {
        return _appOptionsSnapshot.AppTokenOptions;
    }

    public AppFileStorageOptions GetAppFileStorageOptions()
    {
        return _appOptionsSnapshot.AppFileStorageOptions;
    }

    public JwtOptions GetJwtOptions()
    {
        return _jwtOptionsSnapshot;
    }

    public SmtpOptions GetSmtpOptions()
    {
        return _smtpOptionsSnapshot;
    }

    public SmsOptions GetSmsOptions()
    {
        return _smsOptionsSnapshot;
    }

    public ClientAppOptions GetClientAppOptions()
    {
        return _clientAppOptionsSnapshot;
    }

    public string GetSubDomain()
    {
        return _httpContextAccessor.GetTenantFromRequestHeader();
    }

    #endregion Public Methods
}