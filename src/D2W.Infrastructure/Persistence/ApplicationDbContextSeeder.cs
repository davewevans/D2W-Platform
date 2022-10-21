namespace D2W.Infrastructure.Persistence;

public static class ApplicationDbContextSeeder
{
    #region Public Methods

    public static async Task SeedAsync(IPermissionScannerService permissionScannerService)
    {
        await permissionScannerService.ScanBuiltInPermissions();
    }

    #endregion Public Methods
}