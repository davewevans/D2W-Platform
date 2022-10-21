using D2W.Application.Common.Managers;

namespace D2W.Application.UseCases.Identity;

public class RoleUseCase : IRoleUseCase
{
    #region Private Fields

    private readonly ApplicationRoleManager _roleManager;
    private readonly IPermissionUseCase _permissionUseCase;
    private readonly IApplicationDbContext _dbContext;

    #endregion Private Fields

    #region Public Constructors

    public RoleUseCase(ApplicationRoleManager roleManager,
                       IPermissionUseCase permissionUseCase,
                       IApplicationDbContext dbContext)
    {
        _roleManager = roleManager;
        _permissionUseCase = permissionUseCase;
        _dbContext = dbContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<RoleForEdit>> GetRole(GetRoleForEditQuery request)
    {
        if (string.IsNullOrWhiteSpace(request.Id))
            return Envelope<RoleForEdit>.Result.BadRequest(Resource.Invalid_role_Id);

        var role = await _dbContext.Roles.Include(r => r.RoleClaims).Where(r => r.Id == request.Id).FirstOrDefaultAsync();

        if (role == null)
            return Envelope<RoleForEdit>.Result.NotFound(Resource.Unable_to_load_role);

        var roleForEdit = RoleForEdit.MapFromEntity(role);

        return Envelope<RoleForEdit>.Result.Ok(roleForEdit);
    }

    public async Task<Envelope<RolePermissionsForEdit>> GetRolePermissions(GetRolePermissionsForEditQuery request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleId))
            return Envelope<RolePermissionsForEdit>.Result.BadRequest(Resource.Invalid_role_Id);

        var role = await _dbContext.Roles.Include(r => r.RoleClaims).Where(r => r.Id == request.RoleId).FirstOrDefaultAsync();

        if (role == null)
            return Envelope<RolePermissionsForEdit>.Result.NotFound(Resource.Unable_to_load_role);

        var permissions = await _dbContext.ApplicationPermissions.ToListAsync();

        var selectedPermissions = role.RoleClaims.Select(rc => new PermissionItem
        {
            Id = permissions.First(p => p.Name == rc.ClaimValue).Id,
            Name = rc.ClaimValue,
        }).ToList();

        List<PermissionItem> requestedPermissions;

        if (request.LoadingOnDemand)
        {
            var loadedOnDemandPermissions = await _permissionUseCase.GetLoadedOnDemandPermissions(new GetPermissionsQuery { Id = request.PermissionId });
            requestedPermissions = loadedOnDemandPermissions.Payload.Permissions;
        }
        else
        {
            var loadedOneShotPermissions = await _permissionUseCase.GetLoadedOneShotPermissions();
            requestedPermissions = loadedOneShotPermissions.Payload.Permissions;
        }

        var roleForEdit = RolePermissionsForEdit.MapFromEntity(role, selectedPermissions, requestedPermissions);

        return Envelope<RolePermissionsForEdit>.Result.Ok(roleForEdit);
    }

    public async Task<Envelope<RolesResponse>> GetRoles(GetRolesQuery request)
    {
        var query = _roleManager.Roles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(r => r.Name.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy) ? query.SortBy(request.SortBy) : query.OrderBy(r => r.Name);

        var roleItems = await query.Select(q => RoleItem.MapFromEntity(q)).AsNoTracking().ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var rolesResponse = new RolesResponse
        {
            Roles = roleItems
        };

        return Envelope<RolesResponse>.Result.Ok(rolesResponse);
    }

    public async Task<Envelope<CreateRoleResponse>> AddRole(CreateRoleCommand request)
    {
        var role = request.MapToEntity();

        var selectedPermissions = _dbContext.ApplicationPermissions.Where(p => request.SelectedPermissionIds.Contains(p.Id));

        foreach (var selectedPermission in selectedPermissions)
            role.RoleClaims.Add(new ApplicationRoleClaim { ClaimType = "permissions", ClaimValue = selectedPermission.Name });

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            return Envelope<CreateRoleResponse>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError);

        var createRoleResponse = new CreateRoleResponse
        {
            Id = role.Id,
            SuccessMessage = Resource.Role_has_been_created_successfully
        };

        return Envelope<CreateRoleResponse>.Result.Ok(createRoleResponse);
    }

    public async Task<Envelope<string>> EditRole(UpdateRoleCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_role_Id);

        var role = await _roleManager.Roles.Include(r => r.RoleClaims).Where(r => r.Id == request.Id).FirstOrDefaultAsync();

        if (role == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_role);

        if (role.IsStatic)
            return Envelope<string>.Result.ServerError(Resource.Unable_to_update_static_role);

        request.MapToEntity(role);

        await _roleManager.AddOrRemoveRolePermission(request.SelectedPermissionIds, role, _dbContext);

        var result = await _roleManager.UpdateAsync(role);

        await _dbContext.SaveChangesAsync();

        return !result.Succeeded
            ? Envelope<string>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.Role_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteRole(DeleteRoleCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_role_Id);

        var role = await _roleManager.Roles.Include(r => r.RoleClaims).FirstOrDefaultAsync(r => r.Id == request.Id);

        if (role == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_role);

        if (role.IsStatic)
            return Envelope<string>.Result.ServerError(Resource.Unable_to_delete_static_role);

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
            await _roleManager.RemoveExcludedPermissionsFromAllUsers(role, _dbContext);

        return !result.Succeeded
            ? Envelope<string>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.Role_has_been_deleted_successfully);
    }

    #endregion Public Methods
}