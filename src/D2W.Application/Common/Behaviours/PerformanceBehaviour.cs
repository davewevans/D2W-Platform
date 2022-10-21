namespace D2W.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region Private Fields

    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion Private Fields

    #region Public Constructors

    public PerformanceBehaviour(ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds <= 500)
            return response;

        var name = typeof(TRequest).Name;

        _logger.LogWarning("D2W Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
            name, _timer.ElapsedMilliseconds, _httpContextAccessor.GetUserId(), request);

        return response;
    }

    #endregion Public Methods
}