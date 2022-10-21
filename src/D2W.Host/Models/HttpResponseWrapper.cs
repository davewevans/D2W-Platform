namespace D2W.HostApp.Models;

public class HttpResponseWrapper<T>
{
    #region Public Constructors

    public HttpResponseWrapper(T response, bool success, HttpResponseMessage httpResponseMessage)
    {
        Success = success;
        Response = response;
        HttpResponseMessage = httpResponseMessage;
    }

    #endregion Public Constructors

    #region Public Properties

    public bool Success { get; set; }
    public T Response { get; set; }
    public HttpResponseMessage HttpResponseMessage { get; set; }

    #endregion Public Properties
}