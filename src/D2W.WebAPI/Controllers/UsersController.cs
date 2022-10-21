namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[BpAuthorize]
public class UsersController : ApiController
{
    #region Private Fields

    private readonly IWebHostEnvironment _webHostEnvironment;

    #endregion Private Fields

    #region Public Constructors

    public UsersController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpPost("GetUser")]
    public async Task<IActionResult> GetUser(GetUserForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetUsers")]
    public async Task<IActionResult> GetUsers(GetUsersQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("CreateUser")]
    [RequestFormLimits(MultipartBodyLengthLimit = 999999999)]
    public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var response = await Mediator.Send(new DeleteUserCommand { Id = id });
        return TryGetResult(response);
    }

    [HttpPost("GetUserPermissions")]
    public async Task<IActionResult> GetUserPermissions(GetUserPermissionsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GrantOrRevokeUserPermissions")]
    public async Task<IActionResult> GrantOrRevokeUserPermissions(GrantOrRevokeUserPermissionsCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}