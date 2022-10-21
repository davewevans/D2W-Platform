namespace D2W.Infrastructure.Identity.Stores;

public class CustomRoleStore : RoleStore<ApplicationRole>
{
    #region Private Fields

    private readonly IApplicationDbContext _context;

    #endregion Private Fields

    #region Public Constructors

    public CustomRoleStore(IApplicationDbContext context) : base((DbContext)context)
    {
        _context = context;
    }

    #endregion Public Constructors
}