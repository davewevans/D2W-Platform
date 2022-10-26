using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize("Client,Designer")]
    public class ClientController : ApiController
    {

    }
}
