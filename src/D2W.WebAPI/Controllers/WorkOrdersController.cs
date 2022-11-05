using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;

[Route("api/workorders")]
[ApiController]
[Authorize(Roles = "Workroom,Designer")]
public class WorkOrdersController : ApiController
{

}
