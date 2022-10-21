namespace D2W.Application.Common.Interfaces.Services;

public interface IConfigReaderService
{
    #region Public Methods

    AppUserOptions GetAppUserOptions();

    AppLockoutOptions GetAppLockoutOptions();

    AppPasswordOptions GetAppPasswordOptions();

    AppSignInOptions GetAppSignInOptions();

    AppTokenOptions GetAppTokenOptions();

    AppFileStorageOptions GetAppFileStorageOptions();

    JwtOptions GetJwtOptions();

    SmtpOption GetSmtpOption();

    ClientAppOptions GetClientAppOptions();

    string GetSubDomain();

    #endregion Public Methods
}