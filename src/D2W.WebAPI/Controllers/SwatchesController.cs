using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;

[Route("api/swatches")]
[ApiController]
[Authorize(Roles = "Designer")]
public class SwatchesController : ApiController
{
}
