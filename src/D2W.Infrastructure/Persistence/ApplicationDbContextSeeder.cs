using D2W.Application.Common.Managers;

namespace D2W.Infrastructure.Persistence;

public static class ApplicationDbContextSeeder
{
    #region Public Methods

    public static async Task SeedAsync(IPermissionScannerService permissionScannerService)
    {
        await permissionScannerService.ScanBuiltInPermissions();
    }

    public static async Task SeedStaticRoles(ApplicationRoleManager roleManager)
    {
        var adminRole = new ApplicationRole
        {
            Name = "Admin",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var designerRole = new ApplicationRole
        {
            Name = "Designer",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var clientRole = new ApplicationRole
        {
            Name = "Client",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var workroomRole = new ApplicationRole
        {
            Name = "Workroom",
            IsStatic = true,
            IgnoreTenantId = true
        };

        if (!await roleManager.RoleExistsAsync(adminRole.Name))
            await roleManager.CreateAsync(adminRole);

        if (!await roleManager.RoleExistsAsync(designerRole.Name))
            await roleManager.CreateAsync(designerRole);

        if (!await roleManager.RoleExistsAsync(clientRole.Name))
            await roleManager.CreateAsync(clientRole);

        if (!await roleManager.RoleExistsAsync(workroomRole.Name))
            await roleManager.CreateAsync(workroomRole);
    }

    #endregion Public Methods
}