namespace D2W.Infrastructure.Services.IdentityServices;

public class PermissionScannerService : IPermissionScannerService
{
    #region Private Fields

    private readonly ApplicationPartManager _partManager;
    private readonly IApplicationDbContext _dbContext;

    #endregion Private Fields

    #region Public Constructors

    public PermissionScannerService(ApplicationPartManager partManager, IApplicationDbContext dbContext)
    {
        _partManager = partManager;
        _dbContext = dbContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task ScanBuiltInPermissions()
    {
        var feature = new ControllerFeature();
        _partManager.PopulateFeature(feature);
        var controllerTypes = feature.Controllers.SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            .Select(info => new
            {
                Controller = info.DeclaringType.Name.Replace("Controller", string.Empty),
                Action = info.Name,
                Attributes = string.Join(",", info.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                TypeAttributes = string.Join(",", info.DeclaringType.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
            })
            .Where(c => !c.TypeAttributes.Contains("AllowAnonymous") && c.TypeAttributes.Contains("BpAuthorize"))
            .Where(c => !c.Attributes.Contains("AllowAnonymous")).OrderBy(c => c.Controller).ThenBy(c => c.Action).ToList();

        //add root node
        var rootNode = new ApplicationPermission { Name = "Pages" };

        if (!_dbContext.ApplicationPermissions.Any(p => p.Name == rootNode.Name))
            await _dbContext.ApplicationPermissions.AddAsync(rootNode);

        var applicationPermissionsTempList = new List<ApplicationPermission>();

        //add temp root permissions
        foreach (var item in controllerTypes)
        {
            var rootPermission = new ApplicationPermission();

            var rootPermissionName = $"{item.Controller}";
            if (applicationPermissionsTempList.All(p => p.Name != rootPermissionName))
            {
                rootPermission = new ApplicationPermission { Name = rootPermissionName, ParentId = rootNode.Id == Guid.Empty ? _dbContext.ApplicationPermissions.FirstOrDefault(p => p.Name == "Pages").Id : rootNode.Id };

                applicationPermissionsTempList.Add(rootPermission);
            }
        }

        foreach (var rootPermission in applicationPermissionsTempList)
        {
            //add root permissions
            if (!_dbContext.ApplicationPermissions.Any(p => p.Name == rootPermission.Name))
            {
                rootPermission.ParentId = rootNode.Id == Guid.Empty ? _dbContext.ApplicationPermissions.FirstOrDefault(p => p.Name == "Pages").Id : rootNode.Id;

                await _dbContext.ApplicationPermissions.AddAsync(rootPermission);
            }

            //add permissions
            foreach (var type in controllerTypes.Where(ct => ct.Controller == rootPermission.Name))
            {
                var childPermissionName = $"{type.Controller}.{type.Action}";

                var controllerName = childPermissionName.Split(".")[0];

                if (!_dbContext.ApplicationPermissions.Any(p => p.Name == childPermissionName))
                    await _dbContext.ApplicationPermissions.AddAsync(new ApplicationPermission
                    {
                        Name = childPermissionName,
                        ParentId = rootPermission.Id == Guid.Empty ? _dbContext.ApplicationPermissions.FirstOrDefault(p => p.Name == controllerName).Id : rootPermission.Id
                    });
            }
        }

        //remove permissions
        var permissions = await _dbContext.ApplicationPermissions.Where(p => !p.IsUserDefined).Include(p => p.Parent).Include(p => p.Permissions).ToListAsync();

        var permissionsToBeDeleted = permissions.Where(p => controllerTypes.All(ct => $"{ct.Controller}.{ct.Action}" != p.Name) && !p.Permissions.Any());

        foreach (var permission in permissionsToBeDeleted)
            _dbContext.ApplicationPermissions.Remove(permission);

        await _dbContext.SaveChangesAsync();

        var parentPermissionsToBeDeleted = permissions.Where(p => p.Parent?.Name == "Pages" && !p.Permissions.Any());

        foreach (var permission in parentPermissionsToBeDeleted)
            _dbContext.ApplicationPermissions.Remove(permission);

        await _dbContext.SaveChangesAsync();
    }

    #endregion Public Methods
}