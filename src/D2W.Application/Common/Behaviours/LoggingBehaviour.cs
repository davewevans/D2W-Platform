namespace D2W.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    #region Private Fields

    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;

    #endregion Private Fields

    #region Public Constructors

    public LoggingBehaviour(ILogger<TRequest> logger,
                            IHttpContextAccessor httpContextAccessor,
                            IIdentityService identityService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _httpContextAccessor.GetUserId() ?? string.Empty;
        string userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@Email} {@Request}",
            requestName, userId, userName, request);
    }

    #endregion Public Methods
}