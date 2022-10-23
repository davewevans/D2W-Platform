namespace D2W.WebAPI.Controllers;

[CustomAuthorize]
public class PermissionsController : ApiController
{
    #region Public Methods

    [HttpPost("GetPermissions")]
    public async Task<IActionResult> GetPermissions(GetPermissionsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}