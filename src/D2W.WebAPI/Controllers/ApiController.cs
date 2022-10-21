namespace D2W.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    #region Private Fields

    private IMediator _mediator;

    #endregion Private Fields

    #region Protected Properties

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    #endregion Protected Properties

    #region Protected Methods

    protected IActionResult TryGetResult<T>(Envelope<T> envelope)
    {
        if (envelope == null)
            throw new Exception("Unhandled exception.");

        if (!envelope.IsError)
            return base.Ok(new ApiResponse("Success", envelope.Payload));

        if (!envelope.ModelStateErrors.Any())
            throw new ApiProblemDetailsException(envelope.Message, (int)envelope.ResponseType);

        ModelState.Clear();
        envelope.ModelStateErrors.ToList().ForEach(mse => ModelState.AddModelError(mse.Key, mse.Value));
        throw new ApiProblemDetailsException(ModelState);
    }

    #endregion Protected Methods
}