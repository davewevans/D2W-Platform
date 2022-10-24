using D2W.Application.Common.Managers;

namespace D2W.Application.UseCases.Identity;

public class UserUseCase : IUserUseCase
{
    #region Private Fields

    private readonly ApplicationUserManager _userManager;
    private readonly ApplicationRoleManager _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IDemoUserPasswordSetterService _demoUserPasswordSetterService;
    private readonly IConfigReaderService _configReaderService;
    private readonly INotificationService _notificationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPermissionUseCase _permissionUseCase;
    private readonly IStorageProvider _storageProvider;
    private readonly IApplicationDbContext _dbContext;

    #endregion Private Fields

    #region Public Constructors

    public UserUseCase(ApplicationUserManager userManager,
                       ApplicationRoleManager roleManager,
                       SignInManager<ApplicationUser> signInManager,
                       IDemoUserPasswordSetterService demoUserPasswordSetterService,
                       IConfigReaderService configReaderService,
                       INotificationService notificationService,
                       IHttpContextAccessor httpContextAccessor,
                       IPermissionUseCase permissionUseCase,
                       IStorageProvider storageProvider,
                       IApplicationDbContext dbContext)
    {
        _demoUserPasswordSetterService = demoUserPasswordSetterService;
        _configReaderService = configReaderService;
        _notificationService = notificationService;
        _httpContextAccessor = httpContextAccessor;
        _permissionUseCase = permissionUseCase;
        _storageProvider = storageProvider;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<UserForEdit>> GetUser(GetUserForEditQuery request)
    {
        if (string.IsNullOrWhiteSpace(request.Id))
            return Envelope<UserForEdit>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserAttachments)
            .Where(u => u.Id == request.Id)
            .FirstOrDefaultAsync();

        if (user == null)
            return Envelope<UserForEdit>.Result.NotFound(Resource.Unable_to_load_user);

        var assignedRoles = user.UserRoles.Select(AssignedUserRoleItem.MapFromEntity).ToList();

        var assignedAttachments = user.UserAttachments.Select(AssignedUserAttachmentItem.MapFromEntity).ToList();

        var userForEdit = UserForEdit.MapFromEntity(user, assignedRoles, assignedAttachments);

        return Envelope<UserForEdit>.Result.Ok(userForEdit);
    }

    public async Task<Envelope<UsersResponse>> GetUsers(GetUsersQuery request)
    {
        var query = _dbContext.Users.AsQueryable();

        if (request.SelectedRoleIds != null && request.SelectedRoleIds.Count != 0)
        {
            query = (from ur in _dbContext.UserRoles
                     where request.SelectedRoleIds.Contains(ur.RoleId)
                     select ur.User).Distinct();
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(u => u.UserName.Contains(request.SearchText) && u.Email.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy) ? query.SortBy(request.SortBy) : query.OrderBy(u => u.UserName);

        var userItems = await query.Select(u => UserItem.MapFromEntity(u)).AsNoTracking().ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var usersResponse = new UsersResponse
        {
            Users = userItems
        };

        return Envelope<UsersResponse>.Result.Ok(usersResponse);
    }

    public async Task<Envelope<CreateUserResponse>> AddUser(CreateUserCommand request)
    {
        var user = request.MapToEntity();

        if (request.SetRandomPassword)
            request.Password = _userManager.GenerateRandomPassword(6);

        await AssignRolesToUser(request.AssignedRoleIds, user);

        var identityResult = await _userManager.CreateWithFilesAsync(user, request.Password, request.Avatar, request.Attachments, "users", _storageProvider);

        if (!identityResult.Succeeded)
            return Envelope<CreateUserResponse>.Result.AddErrors(identityResult.Errors.ToApplicationResult(), ResponseType.ServerError);

        if (request.MustSendActivationEmail)
        {
            await _userManager.SendActivationEmailAsync(user, _configReaderService, _notificationService, _httpContextAccessor);
        }
        else
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
        }

        var createUserResponse = new CreateUserResponse
        {
            Id = user.Id,
            SuccessMessage = Resource.User_has_been_created_successfully
        };

        return Envelope<CreateUserResponse>.Result.Ok(createUserResponse);
    }

    public async Task<Envelope<string>> EditUser(UpdateUserCommand request)
    {
        var user = await _userManager.Users.Include(u => u.UserRoles)
                                           .ThenInclude(r => r.Role)
                                           .ThenInclude(r => r.RoleClaims)
                                           .Include(u => u.UserAttachments)
                                           .Include(u => u.Claims)
                                           .Where(u => u.Id == request.Id)
                                           .FirstOrDefaultAsync();

        if (user == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_user);

        request.MapToEntity(user);

        //if (request.SetRandomPassword && user.EmailConfirmed)
        //    request.Password = _userManager.GenerateRandomPassword(6);

        //if (!string.IsNullOrWhiteSpace(request.Password))
        //{
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var response = await _userManager.ResetPasswordAsync(request.Password, user, token);
        //    if (response.IsError)
        //        return response;
        //}

        if (!user.IsStatic)
            _userManager.AddOrRemoveUserRoles(request.AssignedRoleIds, user);

        if (request.MustSendActivationEmail && !user.EmailConfirmed)
        {
            await _userManager.SendActivationEmailAsync(user, _configReaderService, _notificationService, _httpContextAccessor);
        }

        //if (!string.IsNullOrWhiteSpace(updateUserCommand.Password))
        //    await _demoUserPasswordSetterService.ResetDefaultPassword(updateUserCommand.Password, user); // Remove this line before going live.

        var updateUserResult = await _userManager.UpdateWithFilesAsync(user, request.Avatar, request.AvatarUri, request.Attachments, request.AttachmentIds, request.Email.ReplaceSpaceAndSpecialCharsWithDashes(), _storageProvider);

        if (user.IsStatic)
            return !updateUserResult.Succeeded
                ? Envelope<string>.Result.AddErrors(updateUserResult.Errors.ToApplicationResult(), ResponseType.ServerError)
                : Envelope<string>.Result.Ok(Resource.User_has_been_updated_successfully_without_updating_his_her_roles_as_the_user_is_static_type);

        return !updateUserResult.Succeeded
            ? Envelope<string>.Result.AddErrors(updateUserResult.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.User_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteUser(DeleteUserCommand request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_user);

        if (user.IsStatic)
            return Envelope<string>.Result.ServerError(Resource.Unable_to_delete_static_user);

        var result = await _userManager.DeleteWithFilesAsync(user, "users", _storageProvider);

        return !result.Succeeded
            ? Envelope<string>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.User_has_been_deleted_successfully);
    }

    public async Task<Envelope<UserPermissionsResponse>> GetUserPermissions(GetUserPermissionsQuery request)
    {
        var user = await _userManager.Users.Include(u => u.Claims)
            .Where(u => u.Id == request.UserId)
            .FirstOrDefaultAsync();

        if (user == null)
            return Envelope<UserPermissionsResponse>.Result.NotFound(Resource.Unable_to_load_user);

        var selectedNonExcludedPermissions = await GetUserNonExcludedPermissions(user);

        IList<PermissionItem> allPermissions;

        if (request.LoadingOnDemand)
        {
            var loadedOnDemandPermissions = await _permissionUseCase.GetLoadedOnDemandPermissions(new GetPermissionsQuery());
            allPermissions = loadedOnDemandPermissions.Payload.Permissions;
        }
        else
        {
            var loadedOneShotPermissions = await _permissionUseCase.GetLoadedOneShotPermissions();
            allPermissions = loadedOneShotPermissions.Payload.Permissions;
        }

        var userPermissionsResponse = new UserPermissionsResponse
        {
            SelectedPermissions = selectedNonExcludedPermissions,
            RequestedPermissions = allPermissions
        };

        return Envelope<UserPermissionsResponse>.Result.Ok(userPermissionsResponse);
    }

    public async Task<List<PermissionItem>> GetUserNonExcludedPermissions(ApplicationUser user)
    {
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

        var userClaims = claimsPrincipal.Claims.ToList(); // To get all inherited and direct claims

        var grantedPermissions = from uc in userClaims
                                 join ap in _dbContext.ApplicationPermissions
                                     on uc.Value equals ap.Name
                                 select new PermissionItem { Id = ap.Id, Name = ap.Name };

        var excludedPermissions = await (from uc in _dbContext.UserClaims
                                         join ap in _dbContext.ApplicationPermissions
                                             on uc.ClaimValue equals ap.Name
                                         where uc.IsExcluded && uc.UserId == user.Id
                                         select new PermissionItem { Id = ap.Id, Name = ap.Name }).ToListAsync();

        var selectedNonExcludedPermissions = grantedPermissions.Except(excludedPermissions, ProjectionEqualityComparer<PermissionItem>.Create(a => a.Id)).ToList();

        return selectedNonExcludedPermissions;
    }

    public async Task<Envelope<string>> GrantOrRevokeUserPermissions(GrantOrRevokeUserPermissionsCommand request)
    {
        var user = await _userManager.Users.Include(u => u.Claims)
                                           .Include(u => u.UserRoles)
                                           .Where(u => u.Id == request.UserId)
                                           .FirstOrDefaultAsync();

        if (user == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_user);

        if (user.IsStatic)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_update_static_user);

        var inheritedDbUserPermissions = (from ur in user.UserRoles.Select(ur => ur.RoleId)
                                          join r in _dbContext.Roles on ur equals r.Id
                                          join rc in _dbContext.RoleClaims on r.Id equals rc.RoleId
                                          join ap in _dbContext.ApplicationPermissions
                                              on rc.ClaimValue equals ap.Name
                                          select ap.Id).ToList();

        var directUserPermissionsIds = request.SelectedPermissionIds.Except(inheritedDbUserPermissions).ToList();

        await _userManager.AddOrRemoveDirectUserPermissionsAsync(directUserPermissionsIds, user, _dbContext);

        _userManager.RemoveInheritedPermissionsAsync(inheritedDbUserPermissions,
                                                     request.SelectedPermissionIds,
                                                     user,
                                                     _dbContext);

        await _userManager.UpdateAsync(user);

        return Envelope<string>.Result.Ok(Resource.User_permissions_have_been_updated_successfully);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task AssignRolesToUser(IList<string> assignedRoleIds, ApplicationUser user)
    {
        var defaultRoles = await _roleManager.Roles.Where(r => r.IsDefault).Select(r => r.Id).ToListAsync();

        _userManager.AssignRolesToUser(assignedRoleIds, user, defaultRoles);
    }

    #endregion Private Methods
}