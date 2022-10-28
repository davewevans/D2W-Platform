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

        var salesRole = new ApplicationRole
        {
            Name = "Sales",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var onboardingSpecilistRole = new ApplicationRole
        {
            Name = "OnboardingSpecialist",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var developerRole = new ApplicationRole
        {
            Name = "Developer",
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

        if (!await roleManager.RoleExistsAsync(salesRole.Name))
            await roleManager.CreateAsync(salesRole);

        if (!await roleManager.RoleExistsAsync(onboardingSpecilistRole.Name))
            await roleManager.CreateAsync(onboardingSpecilistRole);

        if (!await roleManager.RoleExistsAsync(developerRole.Name))
            await roleManager.CreateAsync(developerRole);

        if (!await roleManager.RoleExistsAsync(designerRole.Name))
            await roleManager.CreateAsync(designerRole);

        if (!await roleManager.RoleExistsAsync(clientRole.Name))
            await roleManager.CreateAsync(clientRole);

        if (!await roleManager.RoleExistsAsync(workroomRole.Name))
            await roleManager.CreateAsync(workroomRole);


        #region Demo roles

        // TODO remove for production

        var userRole = new ApplicationRole
        {
            Name = "User",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var auditorRole = new ApplicationRole
        {
            Name = "Auditor",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var accountantRole = new ApplicationRole
        {
            Name = "Accountant",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var ceoRole = new ApplicationRole
        {
            Name = "CEO",
            IsStatic = true,
            IgnoreTenantId = true
        };

        var driverRole = new ApplicationRole
        {
            Name = "Driver",
            IsStatic = true,
            IgnoreTenantId = true
        };

        if (!await roleManager.RoleExistsAsync(userRole.Name))
            await roleManager.CreateAsync(userRole);

        if (!await roleManager.RoleExistsAsync(auditorRole.Name))
            await roleManager.CreateAsync(auditorRole);

        if (!await roleManager.RoleExistsAsync(accountantRole.Name))
            await roleManager.CreateAsync(accountantRole);

        if (!await roleManager.RoleExistsAsync(ceoRole.Name))
            await roleManager.CreateAsync(ceoRole);

        if (!await roleManager.RoleExistsAsync(driverRole.Name))
            await roleManager.CreateAsync(driverRole);

        #endregion

    }

    #endregion Public Methods
}