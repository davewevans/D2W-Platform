namespace D2W.Application.Common.Helpers.Validators;

public class MultiTenantUserValidator : IUserValidator<ApplicationUser>
{
    #region Private Fields

    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public MultiTenantUserValidator(ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
    {
        var userInterfaces = typeof(ApplicationUser).GetInterfaces();

        ThrowExceptionIfNotEligibleForMultitenancy(_tenantResolver, userInterfaces);

        TrySetTenantId(user, userInterfaces, _tenantResolver);

        var isAddOperation = await userManager.FindByIdAsync(user.Id) == null;

        var combinationExists = await CheckCombinationExists(userManager, user, isAddOperation, _tenantResolver);

        return combinationExists
            ? IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateUserName",
                Description = _tenantResolver.TenantMode == TenantMode.MultiTenant
                                            ? Resource.The_specified_username_and_email_are_already_registered_in_the_given_tenant
                                            : Resource.The_specified_username_and_email_are_already_registered
            })
            : IdentityResult.Success;
    }

    #endregion Public Methods

    #region Private Methods

    private static void TrySetTenantId(ApplicationUser user, Type[] userInterfaces, ITenantResolver tenantResolver)
    {
        if (userInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMustHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId() ??
                           throw new ArgumentNullException("Unable to assign null value to User.TenantId.");
            var propertyInfo = user.GetType().GetProperty("TenantId");
            if (propertyInfo != null)
                propertyInfo.SetValue(user, Convert.ChangeType(tenantId, propertyInfo.PropertyType), null);
        }
        else if (userInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMayHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId();
            var propertyInfo = user.GetType().GetProperty("TenantId");
            if (propertyInfo != null && propertyInfo.GetValue(user) == null)
            {
                var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = tenantId == null ? null : Convert.ChangeType(tenantId, type);
                propertyInfo.SetValue(user, safeValue, null);
            }
        }
    }

    private static void ThrowExceptionIfNotEligibleForMultitenancy(ITenantResolver tenantResolver, Type[] userInterfaces)
    {
        switch (tenantResolver.TenantMode)
        {
            case TenantMode.MultiTenant:
                {
                    var userEntityImplementsIHaveTenant = userInterfaces.Any(i => i.Name is nameof(IMustHaveTenant) or nameof(IMayHaveTenant));

                    if (!userEntityImplementsIHaveTenant)
                        throw new ArgumentException("ApplicationUser must implement either IMustHaveTenant or IMayHaveTenant.");
                    break;
                }
        }
    }

    private static async Task<bool> CheckCombinationExists(UserManager<ApplicationUser> manager,
                                                           ApplicationUser user,
                                                           bool isAddOperation,
                                                           ITenantResolver tenantResolver)
    {
        bool combinationExists;
        if (isAddOperation)
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await manager.Users
                    .AnyAsync(u => u.NormalizedEmail == user.UserName.ToUpper()
                                   && u.NormalizedEmail == user.Email.ToUpper()
                                   && EF.Property<ApplicationUser>(u, "TenantId") == user.GetType().GetProperty("TenantId").GetValue(user)),

                TenantMode.SingleTenant => await manager.Users.AnyAsync(u =>
                    u.NormalizedEmail == user.UserName.ToUpper() && u.NormalizedEmail == user.Email.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }
        else
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await manager.Users.Where(u => u.Id != user.Id && EF.Property<ApplicationUser>(u, "TenantId") != null)
                    .AnyAsync(u => u.NormalizedEmail == user.UserName.ToUpper()
                                   && u.NormalizedEmail == user.Email.ToUpper()
                                   && EF.Property<ApplicationUser>(u, "TenantId") == user.GetType().GetProperty("TenantId").GetValue(user)),

                TenantMode.SingleTenant => await manager.Users.Where(u => u.Id != user.Id)
                    .AnyAsync(
                        u => u.NormalizedEmail == user.UserName.ToUpper() && u.NormalizedEmail == user.Email.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }

        return combinationExists;
    }

    #endregion Private Methods
}