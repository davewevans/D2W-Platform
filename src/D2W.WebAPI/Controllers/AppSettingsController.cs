namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[BpAuthorize]
public class AppSettingsController : ApiController
{
    #region Public Methods

    [HttpGet("GetIdentitySettings")]
    public async Task<IActionResult> GetIdentitySettings()
    {
        var response = await Mediator.Send(new GetIdentitySettingsQuery());
        return TryGetResult(response);
    }

    [HttpPut("UpdateIdentitySettings")]
    public async Task<IActionResult> UpdateIdentitySettings(UpdateIdentitySettingsCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpGet("GetFileStorageSettings")]
    public async Task<IActionResult> GetFileStorageSettings()
    {
        var response = await Mediator.Send(new GetFileStorageSettingsQuery());
        return TryGetResult(response);
    }

    [HttpPut("UpdateFileStorageSettings")]
    public async Task<IActionResult> UpdateFileStorageSettings(UpdateFileStorageSettingsCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpGet("GetTokenSettings")]
    public async Task<IActionResult> GetTokenSettings()
    {
        var response = await Mediator.Send(new GetTokenSettingsQuery());
        return TryGetResult(response);
    }

    [HttpPut("UpdateTokenSettings")]
    public async Task<IActionResult> UpdateTokenSettings(UpdateTokenSettingsCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}