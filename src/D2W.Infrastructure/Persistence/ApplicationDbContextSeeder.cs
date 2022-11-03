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

    public static async Task SeedCountries(IApplicationDbContext dbContext)
    {
        // USA
        // Spain
        // Japan
        // England
        // Canada
        // Turkey

        var country1 = new CountryModel
        {
            CountryCode = "US",
            CountryName = "USA"
        };

        var country2 = new CountryModel
        {
            CountryCode = "ES",
            CountryName = "Spain"
        };

        var country3 = new CountryModel
        {
            CountryCode = "JP",
            CountryName = "Japan"
        };

        var country4 = new CountryModel
        {
            CountryCode = "GB",
            CountryName = "United Kingdom"
        };

        var country5 = new CountryModel
        {
            CountryCode = "CA",
            CountryName = "Canada"
        };

        var country6 = new CountryModel
        {
            CountryCode = "TR",
            CountryName = "Turkey"
        };

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country1.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country1);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country2.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country2);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country3.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country3);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country4.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country4);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country5.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country5);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country6.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country6);
        }

        await dbContext.SaveChangesAsync();
    }

    #endregion Public Methods
}