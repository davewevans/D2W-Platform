namespace D2W.WebPortal.Interfaces;

public interface IHttpService
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> Get<TResponse>(string url);

    Task<HttpResponseWrapper<object>> Get<TRequest, TResponse>(string url, TRequest data);

    Task<HttpResponseWrapper<object>> Post<TRequest, TResponse>(string url, TRequest data);

    Task<HttpResponseWrapper<object>> Post<TResponse>(string url);

    Task<HttpResponseWrapper<object>> Put<TRequest, TResponse>(string url, TRequest data);

    Task<HttpResponseWrapper<object>> Delete<TResponse>(string url);

    Task<HttpResponseWrapper<object>> PostFormData<TRequest, TResponse>(string url, MultipartFormDataContent data);

    #endregion Public Methods
}