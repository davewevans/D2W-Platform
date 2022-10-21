namespace D2W.WebPortal.Interfaces.Consumers;

public interface IPermissionsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetPermissions(GetPermissionsQuery request);

    #endregion Public Methods
}