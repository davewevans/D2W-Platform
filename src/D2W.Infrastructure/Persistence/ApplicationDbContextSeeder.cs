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

    public static async Task SeedCountries(IApplicationDbContext dbContext, IConfiguration configuration)
    {
        // United States of America
        // United Kingdom
        // Canada
        // Spain
        // Japan
        // Turkey
        // China
        // Italy
        // France
        // Saudi Arabia
        // Portugal 
        // Germany

        string blobBaseUri = configuration["Blob:BlobBaseUri"];

        var country1 = new CountryModel
        {
            CountryCode = "US",
            CountryName = "United States of America",
            CountryFlagUri = $"{blobBaseUri}/us-flag.png"
        };

        var country2 = new CountryModel
        {
            CountryCode = "ES",
            CountryName = "Spain",
            CountryFlagUri = $"{blobBaseUri}/es-flag.png"
        };

        var country3 = new CountryModel
        {
            CountryCode = "JP",
            CountryName = "Japan",
            CountryFlagUri = $"{blobBaseUri}/jp-flag.png"
        };

        var country4 = new CountryModel
        {
            CountryCode = "GB",
            CountryName = "United Kingdom",
            CountryFlagUri = $"{blobBaseUri}/gb-flag.png"
        };

        var country5 = new CountryModel
        {
            CountryCode = "CA",
            CountryName = "Canada",
            CountryFlagUri = $"{blobBaseUri}/ca-flag.png"
        };

        var country6 = new CountryModel
        {
            CountryCode = "TR",
            CountryName = "Turkey",
            CountryFlagUri = $"{blobBaseUri}/tr-flag.png"
        };

        var country7 = new CountryModel
        {
            CountryCode = "CN",
            CountryName = "China",
            CountryFlagUri = $"{blobBaseUri}/cn-flag.png"
        };

        var country8 = new CountryModel
        {
            CountryCode = "IT",
            CountryName = "Italy",
            CountryFlagUri = $"{blobBaseUri}/it-flag.png"
        };

        var country9 = new CountryModel
        {
            CountryCode = "FR",
            CountryName = "France",
            CountryFlagUri = $"{blobBaseUri}/fr-flag.png"
        };

        var country10 = new CountryModel
        {
            CountryCode = "SA",
            CountryName = "Saudi Arabia",
            CountryFlagUri = $"{blobBaseUri}/sa-flag.png"
        };

        var country11 = new CountryModel
        {
            CountryCode = "PT",
            CountryName = "Portugal",
            CountryFlagUri = $"{blobBaseUri}/pt-flag.png"
        };

        var country12 = new CountryModel
        {
            CountryCode = "DE",
            CountryName = "Germany",
            CountryFlagUri = $"{blobBaseUri}/de-flag.png"
        };

        var country13 = new CountryModel
        {
            CountryCode = "AU",
            CountryName = "Australia",
            CountryFlagUri = $"{blobBaseUri}/au-flag.png"
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

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country7.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country7);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country8.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country8);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country9.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country9);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country10.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country10);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country11.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country11);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country12.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country12);
        }

        if (!dbContext.Countries.Any(c => c.CountryCode.Equals(country13.CountryCode)))
        {
            await dbContext.Countries.AddAsync(country13);
        }

        await dbContext.SaveChangesAsync();
    }

    #endregion Public Methods
}