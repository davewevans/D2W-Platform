namespace D2W.WebAPI.Controllers
{
    [Route("api/workrooms")]
    [ApiController]
    [Authorize(Roles = "Workroom,Designer")]
    public class WorkroomsController : ApiController
    {

    }
}
