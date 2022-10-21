namespace D2W.Application.Common.Helpers.Validators;

public class MultiTenantRoleValidator : RoleValidator<ApplicationRole>
{
    #region Private Fields

    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public MultiTenantRoleValidator(ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    public override async Task<IdentityResult> ValidateAsync(RoleManager<ApplicationRole> manager, ApplicationRole role)
    {
        var roleInterfaces = typeof(ApplicationRole).GetInterfaces();

        ThrowExceptionIfNotEligibleForMultitenancy(_tenantResolver, roleInterfaces);

        TrySetTenantId(role, roleInterfaces, _tenantResolver);

        var isAddOperation = await manager.FindByIdAsync(role.Id) == null;

        var combinationExists = await CheckCombinationExists(manager, role, isAddOperation, _tenantResolver);

        return combinationExists
            ? IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateRoleName",
                Description = _tenantResolver.TenantMode == TenantMode.MultiTenant
                                            ? Resource.The_specified_role_is_already_registered_in_the_given_tenant
                                            : Resource.The_specified_role_is_already_registered
            })
            : IdentityResult.Success;
    }

    #endregion Public Methods

    #region Private Methods

    private static void TrySetTenantId(ApplicationRole role, Type[] roleInterfaces, ITenantResolver tenantResolver)
    {
        if (roleInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMustHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId() ??
                           throw new ArgumentNullException("Unable to assign null value to Role.TenantId.");
            var propertyInfo = role.GetType().GetProperty("TenantId");
            if (propertyInfo != null)
                propertyInfo.SetValue(role, Convert.ChangeType(tenantId, propertyInfo.PropertyType), null);
        }
        else if (roleInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMayHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId();
            var propertyInfo = role.GetType().GetProperty("TenantId");
            if (propertyInfo != null && propertyInfo.GetValue(role) == null)
            {
                var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = tenantId == null ? null : Convert.ChangeType(tenantId, type);
                propertyInfo.SetValue(role, safeValue, null);
            }
        }
    }

    private static void ThrowExceptionIfNotEligibleForMultitenancy(ITenantResolver tenantResolver, Type[] roleInterfaces)
    {
        switch (tenantResolver.TenantMode)
        {
            case TenantMode.MultiTenant:
                {
                    var roleEntityImplementsIHaveTenant = roleInterfaces.Any(i => i.Name is nameof(IMustHaveTenant) or nameof(IMayHaveTenant));

                    if (!roleEntityImplementsIHaveTenant)
                        throw new ArgumentException("ApplicationRole must implement either IMustHaveTenant or IMayHaveTenant.");
                    break;
                }
        }
    }

    private static async Task<bool> CheckCombinationExists(RoleManager<ApplicationRole> roleManager,
                                                           ApplicationRole role,
                                                           bool isAddOperation,
                                                           ITenantResolver tenantResolver)
    {
        bool combinationExists;
        if (isAddOperation)
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await roleManager.Roles.AnyAsync(r => r.NormalizedName == role.Name.ToUpper()
                                                                            && EF.Property<ApplicationRole>(r, "TenantId") == role.GetType().GetProperty("TenantId").GetValue(role)),

                TenantMode.SingleTenant => await roleManager.Roles.AnyAsync(r => r.NormalizedName == role.Name.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }
        else
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await roleManager.Roles.Where(r => r.Id != role.Id)
                    .AnyAsync(r => r.NormalizedName == role.Name.ToUpper() &&
                                   EF.Property<ApplicationRole>(r, "TenantId") == role.GetType().GetProperty("TenantId").GetValue(role)),

                TenantMode.SingleTenant => await roleManager.Roles.Where(r => r.Id != role.Id)
                    .AnyAsync(r => r.NormalizedName == role.Name.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }

        return combinationExists;
    }

    #endregion Private Methods
}