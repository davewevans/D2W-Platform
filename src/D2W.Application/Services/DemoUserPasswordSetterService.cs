using D2W.Application.Common.Managers;

namespace D2W.Application.Services;

public class DemoUserPasswordSetterService : IDemoUserPasswordSetterService
{
    #region Private Fields

    private readonly ApplicationUserManager _userManager;

    #endregion Private Fields

    #region Public Constructors

    public DemoUserPasswordSetterService(ApplicationUserManager userManager)
    {
        _userManager = userManager;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task ResetDefaultPassword(string currentPassword, ApplicationUser user)
    {
        if (user.IsDemo)
            await _userManager.ChangePasswordAsync(user, currentPassword, "123456");
    }

    #endregion Public Methods
}