namespace D2W.Application.Common.Interfaces.Services.DemoUserServices;

public interface IDemoUserPasswordSetterService
{
    #region Public Methods

    Task ResetDefaultPassword(string currentPassword, ApplicationUser user);

    #endregion Public Methods
}