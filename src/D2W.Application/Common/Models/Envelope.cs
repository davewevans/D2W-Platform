namespace D2W.Application.Common.Models;

public enum ResponseType
{
    Ok = 200,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    ServerError = 500
}

public abstract class EnvelopeBase
{
    #region Protected Fields

    protected static readonly object SyncLock = new();

    #endregion Protected Fields

    #region Protected Constructors

    protected EnvelopeBase()
    {
        IsError = false;
        Message = string.Empty;
        ModelStateErrors = new Dictionary<string, string>();
    }

    protected EnvelopeBase(Dictionary<string, string> keyValuePairs)
    {
        IsError = true;
        Message = null;
        ModelStateErrors = keyValuePairs;
    }

    #endregion Protected Constructors

    #region Public Properties

    public bool IsError { get; protected set; }
    public ResponseType ResponseType { get; protected set; }
    public string Message { get; protected set; }
    public Dictionary<string, string> ModelStateErrors { get; protected set; }

    #endregion Public Properties
}

public class Envelope<TResponse> : EnvelopeBase
{
    #region Private Fields

    private static Envelope<TResponse> _payload;

    #endregion Private Fields

    #region Public Constructors

    public Envelope(Dictionary<string, string> keyValuePairs) : base(keyValuePairs)
    {
    }

    public Envelope(TResponse payload) : this(null)
    {
        IsError = false;
        Payload = payload;
    }

    #endregion Public Constructors

    #region Private Constructors

    private Envelope()
    {
        IsError = false;
        Message = string.Empty;
        ModelStateErrors = new Dictionary<string, string>();
    }

    #endregion Private Constructors

    #region Public Properties

    //Useful for safe multi-threading
    public static Envelope<TResponse> Result
    {
        get
        {
            if (_payload != null)
                return _payload;
            lock (SyncLock)
            {
                _payload = new Envelope<TResponse>();
                return _payload;
            }
        }
    }

    public TResponse Payload { get; private set; }
    public bool RollbackDisabled { get; private set; }

    #endregion Public Properties

    #region Public Methods

    public Envelope<TResponse> Ok()
    {
        IsError = false;
        ModelStateErrors = new Dictionary<string, string>();
        ResponseType = ResponseType.Ok;
        Message = "Success";
        return this;
    }

    public Envelope<TResponse> Ok(TResponse payload)
    {
        IsError = false;
        ModelStateErrors = new Dictionary<string, string>();
        Message = "Success";
        Payload = payload;
        return this;
    }

    public Envelope<TResponse> ServerError(string errorMessages = "Internal Server Error", bool rollbackDisabled = false)
    {
        IsError = true;
        RollbackDisabled = rollbackDisabled;
        ModelStateErrors = new Dictionary<string, string>();
        ResponseType = ResponseType.ServerError;
        Message = errorMessages;
        return this;
    }

    public Envelope<TResponse> NotFound(string errorMessages = "Not Found", bool rollbackDisabled = false)
    {
        IsError = true;
        RollbackDisabled = rollbackDisabled;
        ModelStateErrors = new Dictionary<string, string>();
        ResponseType = ResponseType.NotFound;
        Message = errorMessages;
        return this;
    }

    public Envelope<TResponse> BadRequest(string errorMessages = "Bad Request", bool rollbackDisabled = false)
    {
        IsError = true;
        RollbackDisabled = rollbackDisabled;
        ModelStateErrors = new Dictionary<string, string>();
        ResponseType = ResponseType.BadRequest;
        Message = errorMessages;
        return this;
    }

    public Envelope<TResponse> AddErrors(Dictionary<string, string> keyValuePairs, ResponseType responseType, string title = null, bool rollbackDisabled = false)
    {
        IsError = true;
        RollbackDisabled = rollbackDisabled;
        Message = title;
        ResponseType = responseType;
        ModelStateErrors = new Dictionary<string, string>();
        foreach (var failure in keyValuePairs)
            ModelStateErrors.Add(failure.Key, failure.Value);

        return this;
    }

    public Envelope<TResponse> Unauthorized(string errorMessages, bool rollbackDisabled = false)
    {
        IsError = true;
        RollbackDisabled = rollbackDisabled;
        ModelStateErrors = new Dictionary<string, string>();
        ResponseType = ResponseType.Unauthorized;
        Message = errorMessages;
        return this;
    }

    private void WithRollbackDisabled()
    {
        IsError = false;
    }

    #endregion Public Methods
}