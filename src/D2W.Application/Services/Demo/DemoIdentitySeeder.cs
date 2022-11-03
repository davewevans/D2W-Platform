using D2W.Application.Common.Managers;
using D2W.Application.Features.Identity.Account.Commands.RegisterClient;
using D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;

namespace D2W.Application.Services.Demo;

public class DemoIdentitySeeder : IDemoIdentitySeeder
{
    private const string ReadOnlyOfficerRole = "Read-Only-Officer";
    private const string FullPrivilegedOfficerRole = "Full-Privileged-Officer";
    #region Private Fields

    private readonly ApplicationUserManager _userManager;
    private readonly ApplicationRoleManager _roleManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    private IdentityResult _result = new();

    #endregion Private Fields

    #region Public Constructors

    public DemoIdentitySeeder(ApplicationUserManager userManager,
                              ApplicationRoleManager roleManager,
                              IApplicationDbContext dbContext, 
                              IMediator mediator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _mediator = mediator;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<ApplicationUser>> SeedDemoOfficersUsers()
    {
        var fullPrivilegedOfficerRole = new ApplicationRole
        {
            Name = "Full-Privileged-Officer",
        };

        var readOnlyOfficerRole = new ApplicationRole
        {
            Name = "Read-Only-Officer",
        };

        await GrantPermissionsForFullPrivilegedOfficerRole(fullPrivilegedOfficerRole);

        await GrantPermissionsForReadOnlyOfficer(readOnlyOfficerRole);

        if (!await _roleManager.RoleExistsAsync(fullPrivilegedOfficerRole.Name))
        {
            _result = await _roleManager.CreateAsync(fullPrivilegedOfficerRole);

            if (!_result.Succeeded)
                return Envelope<ApplicationUser>.Result.AddErrors(_result.Errors.ToApplicationResult(), ResponseType.BadRequest);
        }

        if (!await _roleManager.RoleExistsAsync(readOnlyOfficerRole.Name))
        {
            _result = await _roleManager.CreateAsync(readOnlyOfficerRole);

            if (!_result.Succeeded)
                return Envelope<ApplicationUser>.Result.AddErrors(_result.Errors.ToApplicationResult(), ResponseType.BadRequest);
        }

        var createOfficersUsersResult = await CreateOfficersUsers();

        return createOfficersUsersResult.IsError ? Envelope<ApplicationUser>.Result.ServerError(createOfficersUsersResult.Message) : Envelope<ApplicationUser>.Result.Ok();
    }

    public async Task SeedDemoClients()
    {
        string[] defaultProfilePics = new[]
        {
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/2DDDE973-40EC-4004-ABC0-73FD4CD6D042-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/2DDDE973-40EC-4004-ABC0-73FD4CD6D042-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/344CFC24-61FB-426C-B3D1-CAD5BCBD3209-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/852EC6E1-347C-4187-9D42-DF264CCF17BF-200w.jpeg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-1026.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-1462.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-1536.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2002.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2096.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2296.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2352.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2373.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2441.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2565.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2684.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2736.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-2779.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-278.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-284.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-3158.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-3197.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-327.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-3813.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-479.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-4795.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-4921.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-5111.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-5399.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-5941.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-5985.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/image-lorem-face-6152.jpg",
            "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/lorem-face-5739.jpg",
        };

        var randomizer = new Random();

        var request1 = new RegisterClientCommand
        {
            FullName = "Karoline Grzesiak",
            Email = "kgrzesiaku@stanford.edu",
            PhoneNumber = "469-743-3316",
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };

        var request2 = new RegisterClientCommand
        {
            FullName = "Aleece McGeever",
            Email = "amcgeeverv@bigcartel.com",
            PhoneNumber = "954-917-0238",
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };


        var request3 = new RegisterClientCommand
        {
            FullName = "Gannie Wardale",
            Email = "gwardalew@ibm.com",
            PhoneNumber = "544-502-9946",
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };

        var request4 = new RegisterClientCommand
        {
            FullName = "Shaine Goodreid",
            Email = "sgoodreidx@newsvine.com",
            PhoneNumber = "171-783-6595",
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };

        var request5 = new RegisterClientCommand
        {
            FullName = "Lethia Waszczyk",
            Email = "lwaszczyky@t.co",
            PhoneNumber = "687-570-5617",
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };


        await _mediator.Send(request1);
        await _mediator.Send(request2);
        await _mediator.Send(request3);
        await _mediator.Send(request4);
        await _mediator.Send(request5);
    }

    public Task SeedDemoWorkrooms()
    {
        //{
        //    "id": 5,
        //    "name": "Kassulke-Kozey",
        //    "email": "ehyder4@csmonitor.com",
        //    "phone": "212-511-7953",
        //    "address": "689 Gateway Terrace",
        //    "city": "New York City",
        //    "state": "New York",
        //    "postalCode": "10009"
        //},
        //{
        //    "id": 8,
        //    "name": "Hansen Group",
        //    "email": "nwhiskerd7@typepad.com",
        //    "phone": "720-845-5418",
        //    "address": "4756 Moland Lane",
        //    "city": "Denver",
        //    "state": "Colorado",
        //    "postalCode": "80255"
        //},

        var request1 = new RegisterWorkroomCommand
        {
            CompanyName = "Hansen Group",
            Email = "lwaszczyky@t.co",
            PhoneNumber = "687-570-5617",
        };


        // RegisterWorkroomCommand
        // await Mediator.Send(request);
        throw new NotImplementedException();
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<Envelope<ApplicationUser>> CreateOfficersUsers()
    {
        var fullPrivilegedOfficer = new ApplicationUser
        {
            Email = "john@demo",
            UserName = "john@demo",
            Name = "John",
            Surname = "Smith",
            JobTitle = "Full Privileged Officer",
            EmailConfirmed = true,
            IsSuspended = false,
            IsDemo = true,
        };

        _result = await _userManager.CreateAsync(fullPrivilegedOfficer, "123456");

        if (!_result.Succeeded)
            return Envelope<ApplicationUser>.Result.AddErrors(_result.Errors.ToApplicationResult(), ResponseType.BadRequest);

        var isFullPrivilegedOfficerRole = await _roleManager.RoleExistsAsync(FullPrivilegedOfficerRole);

        if (isFullPrivilegedOfficerRole)
            _result = await _userManager.AddToRoleAsync(fullPrivilegedOfficer, FullPrivilegedOfficerRole);

        var readOnlyOfficer = new ApplicationUser
        {
            Email = "mandy@demo",
            UserName = "mandy@demo",
            Name = "Mandy",
            Surname = "Moore",
            JobTitle = "Read Only Officer",
            EmailConfirmed = true,
            IsSuspended = false,
            IsDemo = true,
        };

        _result = await _userManager.CreateAsync(readOnlyOfficer, "123456");

        if (!_result.Succeeded)
            return Envelope<ApplicationUser>.Result.AddErrors(_result.Errors.ToApplicationResult(), ResponseType.BadRequest);

        var isReadOnlyOfficerRole = await _roleManager.RoleExistsAsync(ReadOnlyOfficerRole);

        if (isReadOnlyOfficerRole)
            _result = await _userManager.AddToRoleAsync(readOnlyOfficer, ReadOnlyOfficerRole);
        return !_result.Succeeded ? Envelope<ApplicationUser>.Result.AddErrors(_result.Errors.ToApplicationResult(), ResponseType.BadRequest) : Envelope<ApplicationUser>.Result.Ok(fullPrivilegedOfficer);
    }

    private async Task GrantPermissionsForReadOnlyOfficer(ApplicationRole readOnlyOfficerRole)
    {
        var readOnlyOfficerPermissionsList = new List<ApplicationPermission>()
        {
            new()
            {
                Name = "Applicants.GetApplicant"
            },
            new()
            {
                Name = "Applicants.GetApplicants"
            }
        };

        var readOnlyOfficerPermissions = (await _dbContext.ApplicationPermissions.ToListAsync()).Where(p =>
            readOnlyOfficerPermissionsList.Any(fpop => fpop.Name == p.Name));

        foreach (var permission in readOnlyOfficerPermissions)
        {
            readOnlyOfficerRole.RoleClaims.Add(new ApplicationRoleClaim
            {
                ClaimType = "permissions",
                ClaimValue = permission.Name
            });
        }
    }

    private async Task GrantPermissionsForFullPrivilegedOfficerRole(ApplicationRole fullPrivilegedOfficerRole)
    {
        var fullPrivilegedOfficerPermissionsList = new List<ApplicationPermission>()
        {
            new()
            {
                Name = "Applicants"
            },
            new()
            {
                Name = "Applicants.CreateApplicant"
            },
            new()
            {
                Name = "Applicants.DeleteApplicant"
            },
            new()
            {
                Name = "Applicants.GetApplicant"
            },
            new()
            {
                Name = "Applicants.GetApplicants"
            },
            new()
            {
                Name = "Applicants.UpdateApplicant"
            },
        };

        var fullPrivilegedOfficerPermissions = (await _dbContext.ApplicationPermissions.ToListAsync()).Where(p =>
            fullPrivilegedOfficerPermissionsList.Any(fpop => fpop.Name == p.Name));

        foreach (var permission in fullPrivilegedOfficerPermissions)
        {
            fullPrivilegedOfficerRole.RoleClaims.Add(new ApplicationRoleClaim
            {
                ClaimType = "permissions",
                ClaimValue = permission.Name
            });
        }
    }

    #endregion Private Methods
}