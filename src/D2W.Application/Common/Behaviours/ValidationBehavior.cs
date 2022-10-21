namespace D2W.Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : EnvelopeBase
{
    #region Private Fields

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    #endregion Private Fields

    #region Public Constructors

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    #endregion Public Constructors

    #region Public Methods

    public Task<TResponse> Handle(TRequest request,  RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators.Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        return failures.Any()
            ? Errors(failures.ToApplicationResult())
            : next();
    }

    #endregion Public Methods

    #region Private Methods

    private static Task<TResponse> Errors(Dictionary<string, string> failures)
    {
        var responseType = typeof(TResponse);

        var resultType = responseType.GetGenericArguments()[0];

        var response = typeof(Envelope<>).MakeGenericType(resultType);

        return Task.FromResult(Activator.CreateInstance(response, failures) as TResponse);
    }

    #endregion Private Methods
}