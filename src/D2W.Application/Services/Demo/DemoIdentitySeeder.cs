using D2W.Application.Common.Managers;

namespace D2W.Application.Services.Demo;

public class DemoIdentitySeeder : IDemoIdentitySeeder
{
    private const string ReadOnlyOfficerRole = "Read-Only-Officer";
    private const string FullPrivilegedOfficerRole = "Full-Privileged-Officer";
    #region Private Fields

    private readonly ApplicationUserManager _userManager;
    private readonly ApplicationRoleManager _roleManager;
    private readonly IApplicationDbContext _dbContext;

    private IdentityResult _result = new();

    #endregion Private Fields

    #region Public Constructors

    public DemoIdentitySeeder(ApplicationUserManager userManager,
                              ApplicationRoleManager roleManager,
                              IApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
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