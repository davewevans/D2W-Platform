using D2W.Application.Features.Clients.Commands.DeleteClient;
using D2W.Application.Features.Clients.Commands.UpdateClient;
using D2W.Application.Features.Clients.Queries.GetClientForEdit;
using D2W.Application.Features.Clients.Queries.GetClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ApiController
    {
        #region Public Methods

        [Authorize("Client,Designer")]
        [HttpPost("GetClient")]
        public async Task<IActionResult> GetClient(GetClientForEditQuery request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[Authorize("Designer")]
        [HttpPost("GetClients")]
        public async Task<IActionResult> GetClients(GetClientsQuery request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[HttpPost("CreateClient")]
        //public async Task<IActionResult> CreateClient(CreateClientCommand request)
        //{
        //    var response = await Mediator.Send(request);
        //    return TryGetResult(response);
        //}

        [Authorize("Client,Designer")]
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient(UpdateWorkroomCommand request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[HttpDelete("DeleteClient")]
        //public async Task<IActionResult> DeleteClient(string id)
        //{
        //    var response = await Mediator.Send(new DeleteClientCommand { Id = id });
        //    return TryGetResult(response);
        //}


        //[AllowAnonymous]
        //[HttpPost("ExportAsPdf")]
        //public async Task<IActionResult> ExportAsPdf(ExportClientsQuery request)
        //{
        //    var response = await Mediator.Send(request);
        //    return TryGetResult(response);
        //}

        #endregion Public Methods
    }
}
