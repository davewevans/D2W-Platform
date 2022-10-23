namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthorize]
public class RolesController : ApiController
{
    #region Public Methods

    [HttpPost("GetRole")]
    public async Task<IActionResult> GetRole(GetRoleForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetRolePermissions")]
    public async Task<IActionResult> GetRolePermissions(GetRolePermissionsForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetRoles")]
    public async Task<IActionResult> GetRoles(GetRolesQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut("UpdateRole")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("DeleteRole")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var response = await Mediator.Send(new DeleteRoleCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}