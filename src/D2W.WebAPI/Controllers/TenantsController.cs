namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class TenantsController : ApiController
{
    #region Public Methods

    [HttpPost("CreateTenant")]
    public async Task<IActionResult> CreateTenant(CreateTenantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}