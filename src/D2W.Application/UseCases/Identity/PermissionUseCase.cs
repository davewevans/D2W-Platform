namespace D2W.Application.UseCases.Identity;

public class PermissionUseCase : IPermissionUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    #endregion Private Fields

    #region Public Constructors

    public PermissionUseCase(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<PermissionsResponse>> GetLoadedOnDemandPermissions(GetPermissionsQuery request)
    {
        #region ManualProjection

        var permissionItems = await _dbContext.ApplicationPermissions.Include(p => p.Permissions)//To get children.
            .Where(p => request.Id == null ? p.ParentId == null : p.Id == request.Id)
            .OrderBy(p => p.Name)
            .Select(p => PermissionItem.MapFromEntity(p))
            .ToListAsync();

        #endregion ManualProjection

        #region AutoMapper

        //var permissionItems = await _dbContext.ApplicationPermissions.Include(p => p.Permissions)
        //    .Where(p => request.Id == null ? p.ParentId == null : (p.Id == request.Id)).OrderBy(p => p.Name)
        //    .ProjectTo<PermissionItem>(_mapper.ConfigurationProvider)
        //    .ToListAsync();

        #endregion AutoMapper

        var permissionsResponse = new PermissionsResponse
        {
            Permissions = request.Id == null
                ? permissionItems
                : permissionItems.FirstOrDefault()?.Permissions?.OrderBy(p => p.Name).ToList(),
        };

        return Envelope<PermissionsResponse>.Result.Ok(permissionsResponse);
    }

    public async Task<Envelope<PermissionsResponse>> GetLoadedOneShotPermissions()
    {
        #region TreeProjectionUsingAutoMapper

        //var permissionItems = await _dbContext.ApplicationPermissions.OrderBy(p => p.Name).Include(p => p.Parent).ToListAsync();

        //var rootLevelItemsWithSubTree = permissionItems.ToTree((parent, child) => child.ParentId == parent.Id).Children.ToList();

        //var requestedPermissions = _mapper.Map<List<TreeExtensions.ITree<ApplicationPermission>>, List<PermissionItem>>(rootLevelItemsWithSubTree);

        //requestedPermissions = requestedPermissions.OrderBy(p => p.Name).ToList();

        #endregion TreeProjectionUsingAutoMapper

        #region TreeProjectionUsingRecursiveExpression

        var query = _dbContext.ApplicationPermissions.AsQueryable();

        query = query.Where(c => c.Parent == null);

        var maxParentCount = await GetPermissionsMaxParentCount();

        var requestedPermissions = await query.Select(GetPermissionsRecursively(maxParentCount)).ToListAsync();

        #endregion TreeProjectionUsingRecursiveExpression

        var permissionsResponse = new PermissionsResponse
        {
            Permissions = requestedPermissions,
        };

        return Envelope<PermissionsResponse>.Result.Ok(permissionsResponse);
    }

    private static Expression<Func<ApplicationPermission, PermissionItem>> GetPermissionsRecursively(int maxParentCount, int currentParentCount = 0)
    {
        currentParentCount++;

        Expression<Func<ApplicationPermission, PermissionItem>> result = permission => new PermissionItem()
        {
            Id = permission.Id,
            Name = permission.Name,
            ParentId = permission.ParentId,
            HasChildren = permission.Permissions.Count != 0,
            IsUserDefined = permission.IsUserDefined,
            IsRoot = permission.Parent == null,
            Permissions = currentParentCount == maxParentCount
                ? null
                : permission.Permissions.AsQueryable()
                    .Select(GetPermissionsRecursively(maxParentCount, currentParentCount))
                    .OrderBy(p => p.Name).ToList()
        };

        return result;
    }

    private async Task<int> GetPermissionsMaxParentCount()
    {
        return (await _dbContext.ApplicationPermissions.Select(p => p.ParentId).ToListAsync()).DistinctBy(p => p).Count() + 1;
    }

    #endregion Public Methods
}