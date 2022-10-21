using IsolationLevel = System.Transactions.IsolationLevel;

namespace D2W.Application.Common.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region Private Fields

    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
    private readonly IApplicationDbContext _dbContext;

    #endregion Private Fields

    #region Public Constructors

    public TransactionBehaviour(ILogger<TransactionBehaviour<TRequest, TResponse>> logger, IApplicationDbContext dbContext)
    {
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        _dbContext = dbContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetType().Name;

        if (!typeName.EndsWith("Command", true, CultureInfo.CurrentCulture))
            return await next();

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };

        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            await strategy.ExecuteAsync(async () =>
            {
                _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                response = await next();

                var isErrorProperty = response.GetType().GetProperty("IsError");

                bool isError = isErrorProperty != null ? (bool)isErrorProperty?.GetValue(response, null) : false;

                var rollbackDisabledProperty = response.GetType().GetProperty("RollbackDisabled");

                bool rollbackDisabled = rollbackDisabledProperty != null ? (bool)rollbackDisabledProperty?.GetValue(response, null) : false;

                if (isError is false || rollbackDisabled)
                {
                    scope.Complete();
                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
                }
            });
        }

        return response;
    }

    #endregion Public Methods
}