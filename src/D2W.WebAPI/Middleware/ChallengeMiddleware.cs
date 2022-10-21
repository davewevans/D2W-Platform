namespace D2W.WebAPI.Middleware;

public static class ChallengeMiddlewareExtensions
{
    #region Public Methods

    public static IApplicationBuilder UseChallenge(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ChallengeMiddleware>();
    }

    #endregion Public Methods
}

public class ChallengeMiddleware
{
    #region Private Fields

    private readonly RequestDelegate _request;

    #endregion Private Fields

    #region Public Constructors

    public ChallengeMiddleware(RequestDelegate RequestDelegate)
    {
        _request = RequestDelegate ?? throw new ArgumentNullException(nameof(RequestDelegate), nameof(RequestDelegate) + " is required");
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context), nameof(context) + " is required");

        switch (context.Response.StatusCode)
        {
            case 401:
                throw new ApiProblemDetailsException(string.Format("Resource.You_are_not_authorized", context.Request.GetDisplayUrl()), 401);

            case 403:
                throw new ApiProblemDetailsException(string.Format(Resource.You_are_forbidden, context.Request.GetDisplayUrl()), 403);
        }

        await _request(context);
    }

    #endregion Public Methods
}