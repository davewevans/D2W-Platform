using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;
[Route("api/emails")]
[ApiController]
[Authorize(Roles = "Designer")]
public class EmailsController : ApiController
{
}
