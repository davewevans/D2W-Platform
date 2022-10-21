namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ManageController : ApiController
{
    #region Public Methods

    [HttpGet("GetCurrentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var response = await Mediator.Send(new GetCurrentUserForEditQuery());
        return TryGetResult(response);
    }

    [HttpGet("GetUserAvatar")]
    public async Task<IActionResult> GetUserAvatar()
    {
        var response = await Mediator.Send(new GetUserAvatarForEditQuery());
        return TryGetResult(response);
    }

    [HttpPut("UpdateUserProfile")]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("UpdateUserAvatar")]
    public async Task<IActionResult> UpdateUserAvatar([FromForm] UpdateUserAvatarCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ChangeEmail")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPut("ConfirmEmailChange")]
    public async Task<IActionResult> ConfirmEmailChange(ConfirmEmailChangeCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("Disable2Fa")]
    public async Task<IActionResult> Disable2Fa()
    {
        var response = await Mediator.Send(new Disable2FaCommand());
        return TryGetResult(response);
    }

    [HttpGet("LoadSharedKeyAndQrCodeUri")]
    public async Task<IActionResult> LoadSharedKeyAndQrCodeUri()
    {
        var response = await Mediator.Send(new LoadSharedKeyAndQrCodeUriQuery());
        return TryGetResult(response);
    }

    [HttpPost("EnableAuthenticator")]
    public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("ResetAuthenticator")]
    public async Task<IActionResult> ResetAuthenticator()
    {
        var response = await Mediator.Send(new ResetAuthenticatorCommand());
        return TryGetResult(response);
    }

    [HttpPost("SetPassword")]
    public async Task<IActionResult> SetPassword(SetPasswordCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpGet("DownloadPersonalData")]
    public async Task<IActionResult> DownloadPersonalData()
    {
        var response = await Mediator.Send(new DownloadPersonalDataQuery());
        return TryGetResult(response);
    }

    [HttpGet("CheckUser2FaState")]
    public async Task<IActionResult> CheckUser2FaState()
    {
        var response = await Mediator.Send(new CheckUser2FaStateQuery());
        return TryGetResult(response);
    }

    [HttpGet("GenerateRecoveryCodes")]
    public async Task<IActionResult> GenerateRecoveryCodes()
    {
        var response = await Mediator.Send(new GenerateRecoveryCodesQuery());
        return TryGetResult(response);
    }

    [HttpGet("Get2FaState")]
    public async Task<IActionResult> Get2FaState()
    {
        var response = await Mediator.Send(new Get2FaStateQuery());
        return TryGetResult(response);
    }

    [HttpPost("DeletePersonalData")]
    public async Task<IActionResult> DeletePersonalData(DeletePersonalDataCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpGet("RequirePassword")]
    public async Task<IActionResult> RequirePassword()
    {
        var response = await Mediator.Send(new RequirePasswordQuery());
        return TryGetResult(response);
    }

    #endregion Public Methods
}