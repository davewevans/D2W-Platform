namespace D2W.WebPortal.Interfaces.Consumers;

public interface IDashboardClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetHeadlinesData();

    #endregion Public Methods
}