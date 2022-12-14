using D2W.Application.Common.Managers;
using D2W.Application.Features.Fabrics.Commands.CreateFabric;
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

    private static Random _randomizer = new Random();

    #endregion Private Fields

    private readonly string[] _defaultProfilePics = new[]
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
        var request1 = new RegisterClientCommand
        {
            FullName = "Karoline Grzesiak",
            Email = "kgrzesiaku@stanford.edu",
            PhoneNumber = "469-743-3316",
            AvatarUri = GetRandomProfilePic()
        };

        var request2 = new RegisterClientCommand
        {
            FullName = "Aleece McGeever",
            Email = "amcgeeverv@bigcartel.com",
            PhoneNumber = "954-917-0238",
            AvatarUri = "https://eu.ui-avatars.com/api/?name=Aleece+McGeever&size=250"
        };


        var request3 = new RegisterClientCommand
        {
            FullName = "Gannie Wardale",
            Email = "gwardalew@ibm.com",
            PhoneNumber = "544-502-9946",
            AvatarUri = GetRandomProfilePic()
        };

        var request4 = new RegisterClientCommand
        {
            FullName = "Shaine Goodreid",
            Email = "sgoodreidx@newsvine.com",
            PhoneNumber = "171-783-6595",
            AvatarUri = GetRandomProfilePic()
        };

        var request5 = new RegisterClientCommand
        {
            FullName = "Lethia Waszczyk",
            Email = "lwaszczyky@t.co",
            PhoneNumber = "687-570-5617",
            AvatarUri = GetRandomProfilePic()
        };


        await _mediator.Send(request1);
        await _mediator.Send(request2);
        await _mediator.Send(request3);
        await _mediator.Send(request4);
        await _mediator.Send(request5);
    }

    public async Task SeedDemoWorkrooms()
    {
        var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.CountryCode.Equals("US"));

        var request1 = new RegisterWorkroomCommand
        {
            CompanyName = "Hansen Group",
            Email = "ehyder4@csmonitor.com",
            PhoneNumber = "212-511-7953",
            AltPhoneNumber = "212-504-4624",
            AddressLine1 = "689 Gateway Terrace",
            City = "New York City",
            Region = "New York",
            PostalCode = "10009",
            CountryId = country.Id
        };

        var request2 = new RegisterWorkroomCommand
        {
            CompanyName = "Kassulke-Kozey",
            Email = "kyakovitch3g@narod.ru",
            PhoneNumber = "545-956-0020",
            AltPhoneNumber = "545-956-0020",
            AddressLine1 = "4756 Moland Lane",
            City = "Denver",
            Region = "Colorado",
            PostalCode = "10009",
            CountryId = country.Id
        };


        await _mediator.Send(request1);
        await _mediator.Send(request2);
    }

    public async Task SeedDemoFabrics()
    {
        var fabric1 = new CreateFabricCommand
        {
            ManufacturerName = "Fabricut Contract",
            BrandName = "Fabricut Contract",
            ProductNumber = "12345678",
            Pattern = "Percival On Tx Spice",
            Color = "Orange / Spice",
            SwatchImageUri = "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/PercivalOnTxSpice.jpg",
            CostPerYard = 2.5f,
            CostPerMeter = 2.5f,
            IsRepeating = false,
            VerticalRepeatInInches = 27,
            VerticalRepeatInCentimeters = 68.58f,
            HorizontalRepeatInInches = 27,
            HorizontalRepeatInCentimeters = 68.58f,
            WidthInInches = 54,
            WidthInCentimeters = 137.16f
        };
        var fabric2 = new CreateFabricCommand
        {
            ManufacturerName = "Fabricut Contract",
            BrandName = "Fabricut Contract",
            ProductNumber = "12345678",
            Pattern = "Shay On Cn Sand",
            Color = "Beige",
            SwatchImageUri = "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/Shay-On-Cn-Sand.jpg",
            CostPerYard = 1.75f,
            CostPerMeter = 1.75f,
            IsRepeating = false,
            VerticalRepeatInInches = 6,
            VerticalRepeatInCentimeters = 15.24f,
            HorizontalRepeatInInches = 4.5f,
            HorizontalRepeatInCentimeters = 11.43f,
            WidthInInches = 54,
            WidthInCentimeters = 137.16f
        };
        var fabric3 = new CreateFabricCommand
        {
            ManufacturerName = "Fabricut Contract",
            BrandName = "Fabricut Contract",
            ProductNumber = "12345678",
            Pattern = "Merritt On Cn Morning Fog",
            Color = "Beige",
            SwatchImageUri = "https://d2wdevstorage.blob.core.windows.net/d2wdevblob/Merritt-On-Cn-Morning-Fog.jpg",
            CostPerYard = 1.25f,
            CostPerMeter = 1.25f,
            IsRepeating = false,
            VerticalRepeatInInches = 43,
            VerticalRepeatInCentimeters = 109.22f,
            HorizontalRepeatInInches = 27,
            HorizontalRepeatInCentimeters = 68.58f,
            WidthInInches = 54,
            WidthInCentimeters = 137.16f
        };
        

        await _mediator.Send(fabric1);
        await _mediator.Send(fabric2);
        await _mediator.Send(fabric3);
    }

    public string GetRandomProfilePic()
    {
        return _defaultProfilePics[_randomizer.Next(_defaultProfilePics.Length)];
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