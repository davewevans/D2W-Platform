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

    SmtpOptions GetSmtpOptions();

    SmsOptions GetSmsOptions();

    ClientAppOptions GetClientAppOptions();

    string GetSubDomain();

    #endregion Public Methods
}