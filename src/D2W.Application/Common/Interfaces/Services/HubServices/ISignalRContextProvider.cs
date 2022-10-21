namespace D2W.Application.Common.Interfaces.Services.HubServices;

public interface ISignalRContextProvider
{
    #region Public Methods

    string GetHostName(HubCallerContext hubCallerContext);

    Guid? GetTenantId(HubCallerContext hubCallerContext);

    string GetTenantName(HubCallerContext hubCallerContext);

    string GetUserNameIdentifier(HubCallerContext hubCallerContext);

    string GetUserName(HubCallerContext hubCallerContext);

    void SetTenantIdViaTenantResolver(HubCallerContext context);

    #endregion Public Methods
}