namespace D2W.WebAPI.Controllers
{
    [Route("api/workroom")]
    [ApiController]
    [Authorize(Roles = "Workroom,Designer")]
    public class WorkroomController : ApiController
    {

    }
}
