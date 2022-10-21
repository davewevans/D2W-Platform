namespace D2W.Infrastructure.Identity.Stores;

public class CustomUserStore : UserStore<ApplicationUser>
{
    #region Private Fields

    private readonly IApplicationDbContext _context;

    #endregion Private Fields

    #region Public Constructors

    public CustomUserStore(IApplicationDbContext context) : base((DbContext)context)
    {
        _context = context;
    }

    #endregion Public Constructors
}