using D2W.Application.Common.Interfaces.UseCases.Identity;
using D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
using D2W.Application.Features.Identity.Account.Commands.RegisterClient;
using D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;
using D2W.Application.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

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

    [HttpPost("LoginSendVerificationCode")]
    public async Task<IActionResult> SendLoginCodeVerification(SendLoginVerificationCodeCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("LoginCodeVerification")]
    public async Task<IActionResult> LoginWithCodeVerification(LoginWithCodeCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    //[HttpPost("LoginWith2Fa")]
    //public async Task<IActionResult> LoginWith2Fa(LoginWith2FaCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

    //[HttpPost("LoginWithRecoveryCode")]
    //public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

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

    [HttpPost("ClientRegister")]
    public async Task<IActionResult> RegisterClient(RegisterClientCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("WorkroomRegister")]
    public async Task<IActionResult> RegisterWorkroom(RegisterWorkroomCommand request)
    {
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