namespace D2W.HostApp.Services;

public class HttpService : IHttpService
{
    #region Private Fields

    private readonly HttpClient _httpClient;

    #endregion Private Fields

    #region Public Constructors

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion Public Constructors

    #region Private Properties

    private static JsonSerializerOptions DefaultJsonSerializerOptions => new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    #endregion Private Properties

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> Get<TResponse>(string url)
    {
        using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Get<TRequest, TResponse>(string url, TRequest data)
    {
        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Content = stringContent,
        };

        using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Post<TRequest, TResponse>(string url, TRequest data)
    {
        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PostAsync(url, stringContent).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> PostFormData<TRequest, TResponse>(string url, MultipartFormDataContent data)
    {
        using var response = await _httpClient.PostAsync(url, data).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Post<TResponse>(string url)
    {
        using var response = await _httpClient.PostAsync(url, null).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Put<TRequest, TResponse>(string url, TRequest data)
    {
        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PutAsync(url, stringContent).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Delete<TResponse>(string url)
    {
        using var response = await _httpClient.DeleteAsync(url).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Delete(string url)
    {
        var responseHttp = await _httpClient.DeleteAsync(url).ConfigureAwait(false);

        return new HttpResponseWrapper<object>(null, responseHttp.IsSuccessStatusCode, responseHttp);
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
    {
        var responseString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(responseString, options);
    }

    #endregion Private Methods
}