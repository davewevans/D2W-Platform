namespace D2W.WebAPI.Controllers;

public class AccountController : ApiController
{
    #region Public Methods

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("LoginWith2Fa")]
    public async Task<IActionResult> LoginWith2Fa(LoginWith2FaCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("LoginWithRecoveryCode")]
    public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var createTenantResponse = await Mediator.Send(new CreateTenantCommand
        {
            FullName = request.FullName,
        });

        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("RegisterBetaUser")]
    public async Task<IActionResult> RegisterBetaUser(RegisterCommand request)
    {
        // TODO check for number of beta user
        // get max beta users from config
        // if maxed out return payload from here
        // don't create tenant if reached max beta users


        var createTenantResponse = await Mediator.Send(new CreateTenantCommand
        {
            FullName = request.FullName,
        });

        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ResendEmailConfirmation")]
    public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgetPasswordCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}