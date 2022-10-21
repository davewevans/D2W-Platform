namespace D2W.Application.Common.Interfaces.Services;

public interface IPermissionScannerService
{
    #region Public Methods

    Task ScanBuiltInPermissions();

    #endregion Public Methods
}