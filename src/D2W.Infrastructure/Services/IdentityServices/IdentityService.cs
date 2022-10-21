namespace D2W.Infrastructure.Services.IdentityServices;

public class IdentityService : IIdentityService
{
    #region Private Fields

    private readonly UserManager<ApplicationUser> _userManager;

    #endregion Private Fields

    #region Public Constructors

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<string> GetUserNameAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return "Anonymous";

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user?.UserName ?? "Anonymous";
    }

    #endregion Public Methods
}